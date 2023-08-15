#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <arpa/inet.h>
#include <time.h>
#include <unistd.h>

#define MAX_TABLE_SIZE 1024
#define MAX_PACKET_SIZE 4096
#define TIMEOUT_SECONDS 60

struct Entry {
    struct sockaddr_in address;
    time_t lastUpdateTime;
};

struct Entry table[MAX_TABLE_SIZE];
int numEntries = 0;

int isAddressPresent(struct sockaddr_in clientAddress) {
    for (int i = 0; i < numEntries; i++) {
        if (memcmp(&table[i].address, &clientAddress, sizeof(struct sockaddr_in)) == 0) {
            return i; // Return the index of the entry if found
        }
    }
    return -1; // Return -1 if not found
}

void addToTable(struct sockaddr_in clientAddress) {
    if (numEntries >= MAX_TABLE_SIZE) {
        printf("Table is full. Cannot add more entries.\n");
        return;
    }
    int index = isAddressPresent(clientAddress);
    if (index == -1) {
        table[numEntries].address = clientAddress;
        table[numEntries].lastUpdateTime = time(NULL);
        numEntries++;
    }
    else {
        table[index].lastUpdateTime = time(NULL);
    }
}

void removeExpiredEntries() {
    time_t currentTime = time(NULL);
    for (int i = 0; i < numEntries; i++) {
        if (currentTime - table[i].lastUpdateTime > TIMEOUT_SECONDS) {
            printf("Removing entry for %s:%d (inactive for more than 60 seconds).\n",
                inet_ntoa(table[i].address.sin_addr), ntohs(table[i].address.sin_port));

            // Remove the entry by shifting subsequent entries back
            memmove(&table[i], &table[i + 1], (numEntries - i - 1) * sizeof(struct Entry));
            numEntries--;
            i--; // Recheck the same index since we shifted the elements
        }
    }
}

void forwardPacketToAll(int sockfd, char* packet, int packetSize) {
    for (int i = 0; i < numEntries; i++) {
        sendto(sockfd, packet, packetSize, 0, (struct sockaddr*)&table[i].address, sizeof(struct sockaddr_in));
    }
}

int main() {
    int sockfd;
    struct sockaddr_in serverAddr, clientAddr;
    char packet[MAX_PACKET_SIZE];
    int recvBytes;

    // Create UDP socket
    if ((sockfd = socket(AF_INET, SOCK_DGRAM, 0)) < 0) {
        perror("socket");
        exit(EXIT_FAILURE);
    }

    // Initialize server address structure
    memset(&serverAddr, 0, sizeof(serverAddr));
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_addr.s_addr = htonl(INADDR_ANY);
    serverAddr.sin_port = htons(50027);

    // Bind socket to the server address
    if (bind(sockfd, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) < 0) {
        perror("bind");
        close(sockfd);
        exit(EXIT_FAILURE);
    }

    printf("UDP server is listening on port %d...\n", ntohs(serverAddr.sin_port));

    while (1) {

        // Receive packet
        socklen_t clientAddrLen = sizeof(clientAddr);
        recvBytes = recvfrom(sockfd, packet, sizeof(packet), 0, (struct sockaddr*)&clientAddr, &clientAddrLen);

        if (recvBytes <= 0) {
            continue;
        }

        // printf("Received %d bytes from %s:%d\n", recvBytes, inet_ntoa(clientAddr.sin_addr), ntohs(clientAddr.sin_port));

        // Add client address to the table if it doesn't exist or update its last update time if it exists
        addToTable(clientAddr);

        // Remove expired entries from the table
        removeExpiredEntries();

        // Forward the packet to all entries in the table
        forwardPacketToAll(sockfd, packet, recvBytes);
    }

    close(sockfd);
    return 0;
}
