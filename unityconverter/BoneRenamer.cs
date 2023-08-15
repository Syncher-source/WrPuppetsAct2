using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoneRenamerEditor : EditorWindow
{
    private GameObject skeletonObject;
    private Animator animator;
    private static Dictionary<string, string> bonesMap;

    [MenuItem("WrPuppets/BoneRenamer")]
    private static void Init()
    {

        bonesMap = new System.Collections.Generic.Dictionary<string, string>();
        bonesMap["Hips"] = "J_Bip_C_Hips";
        bonesMap["Spine"] = "J_Bip_C_Spine";
        bonesMap["Chest"] = "J_Bip_C_Chest";
        bonesMap["UpperChest"] = "J_Bip_C_UpperChest";
        bonesMap["Neck"] = "J_Bip_C_Neck";
        bonesMap["Head"] = "J_Bip_C_Head";
        bonesMap["LeftEye"] = "J_Adj_L_FaceEye";
        bonesMap["RightEye"] = "J_Adj_R_FaceEye";
        bonesMap["LeftUpperLeg"] = "J_Bip_L_UpperLeg";
        bonesMap["RightUpperLeg"] = "J_Bip_R_UpperLeg";
        bonesMap["LeftLowerLeg"] = "J_Bip_L_LowerLeg";
        bonesMap["RightLowerLeg"] = "J_Bip_R_LowerLeg";
        bonesMap["LeftFoot"] = "J_Bip_L_Foot";
        bonesMap["RightFoot"] = "J_Bip_R_Foot";
        bonesMap["LeftToes"] = "J_Bip_L_ToeBase";
        bonesMap["RightToes"] = "J_Bip_R_ToeBase";
        bonesMap["LeftShoulder"] = "J_Bip_L_Shoulder";
        bonesMap["RightShoulder"] = "J_Bip_R_Shoulder";
        bonesMap["LeftUpperArm"] = "J_Bip_L_UpperArm";
        bonesMap["RightUpperArm"] = "J_Bip_R_UpperArm";
        bonesMap["LeftLowerArm"] = "J_Bip_L_LowerArm";
        bonesMap["RightLowerArm"] = "J_Bip_R_LowerArm";
        bonesMap["LeftHand"] = "J_Bip_L_Hand";
        bonesMap["RightHand"] = "J_Bip_R_Hand";
        bonesMap["Left Thumb Proximal"] = "J_Bip_L_Thumb1";
        bonesMap["Left Thumb Intermediate"] = "J_Bip_L_Thumb2";
        bonesMap["Left Thumb Distal"] = "J_Bip_L_Thumb3";
        bonesMap["Left Index Proximal"] = "J_Bip_L_Index1";
        bonesMap["Left Index Intermediate"] = "J_Bip_L_Index2";
        bonesMap["Left Index Distal"] = "J_Bip_L_Index3";
        bonesMap["Left Middle Proximal"] = "J_Bip_L_Middle1";
        bonesMap["Left Middle Intermediate"] = "J_Bip_L_Middle2";
        bonesMap["Left Middle Distal"] = "J_Bip_L_Middle3";
        bonesMap["Left Ring Proximal"] = "J_Bip_L_Ring1";
        bonesMap["Left Ring Intermediate"] = "J_Bip_L_Ring2";
        bonesMap["Left Ring Distal"] = "J_Bip_L_Ring3";
        bonesMap["Left Little Proximal"] = "J_Bip_L_Little1";
        bonesMap["Left Little Intermediate"] = "J_Bip_L_Little2";
        bonesMap["Left Little Distal"] = "J_Bip_L_Little3";
        bonesMap["Right Thumb Proximal"] = "J_Bip_R_Thumb1";
        bonesMap["Right Thumb Intermediate"] = "J_Bip_R_Thumb2";
        bonesMap["Right Thumb Distal"] = "J_Bip_R_Thumb3";
        bonesMap["Right Index Proximal"] = "J_Bip_R_Index1";
        bonesMap["Right Index Intermediate"] = "J_Bip_R_Index2";
        bonesMap["Right Index Distal"] = "J_Bip_R_Index3";
        bonesMap["Right Middle Proximal"] = "J_Bip_R_Middle1";
        bonesMap["Right Middle Intermediate"] = "J_Bip_R_Middle2";
        bonesMap["Right Middle Distal"] = "J_Bip_R_Middle3";
        bonesMap["Right Ring Proximal"] = "J_Bip_R_Ring1";
        bonesMap["Right Ring Intermediate"] = "J_Bip_R_Ring2";
        bonesMap["Right Ring Distal"] = "J_Bip_R_Ring3";
        bonesMap["Right Little Proximal"] = "J_Bip_R_Little1";
        bonesMap["Right Little Intermediate"] = "J_Bip_R_Little2";
        bonesMap["Right Little Distal"] = "J_Bip_R_Little3";


        BoneRenamerEditor window = (BoneRenamerEditor)EditorWindow.GetWindow(typeof(BoneRenamerEditor));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("VRoid naming convetion bones renamer", EditorStyles.boldLabel);
        skeletonObject = EditorGUILayout.ObjectField("Avatar root node", skeletonObject, typeof(GameObject), true) as GameObject;
        animator = EditorGUILayout.ObjectField("Avatar root node", animator, typeof(Animator), true) as Animator;

        if (GUILayout.Button("Rename Bones"))
        {
            RenameBones();
        }
    }

    private void RenameBones()
    {
        if (skeletonObject == null)
        {
            Debug.LogError("Skeleton Object is not assigned.");
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned.");
            return;
        }

        HumanPoseHandler poseHandler = new HumanPoseHandler(animator.avatar, animator.transform);
        HumanPose humanPose = new HumanPose();
        poseHandler.GetHumanPose(ref humanPose);

        HumanDescription description = animator.avatar.humanDescription;

        Transform[] bones = skeletonObject.GetComponentsInChildren<Transform>();

        if (bones.Length > 1)
        {
            bones[1].name = "Root";
        }

        foreach (Transform bone in bones)
        {
            for (int i = 0; i < description.human.Length; i++)
            {
                if (bone.name == description.human[i].boneName)
                {
                    bone.name = bonesMap[description.human[i].humanName];
                }
            }
        }

    }


}