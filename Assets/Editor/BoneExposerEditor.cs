using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace UnityToolKit
{
    [CustomEditor(typeof(BoneExposer))]
    public class BoneExposerEditor : Editor
    {
        private Transform copyRoot;
        private SkinnedMeshRenderer src;

        public override void OnInspectorGUI()
        {
            // Get the renderer from the target object
            var renderer = (target as BoneExposer).gameObject.GetComponent<SkinnedMeshRenderer>();

            // If there is no renderer, we can't do anything
            if (!renderer)
            {
                EditorGUILayout.HelpBox("SortingLayerExposed must be added to a game object that has a renderer.",
                    MessageType.Error);
                return;
            }

            var bones = renderer.bones;
            for (var index = 0; index < bones.Length; index++)
            {
                Transform bone = bones[index];
                bones[index] = EditorGUILayout.ObjectField("bone " + index, bone, typeof(Transform), true) as Transform;
            }

            copyRoot = EditorGUILayout.ObjectField("From root", copyRoot, typeof(Transform), true) as Transform;
            if (copyRoot)
            {
                src = EditorGUILayout.ObjectField("Src Bones", src, typeof(SkinnedMeshRenderer), true) as
                    SkinnedMeshRenderer;

                var mybones = new List<Transform>();
                if (src)
                {
                    for (int i = 0; i < src.bones.Length; i++)
                    {
                        var bone = src.bones[i];
                        if (bone)
                            mybones.Add(copyRoot.GetComponentsInChildren<Transform>()
                                .FirstOrDefault(t => t.name == bone.name));
                    }
                    renderer.bones = mybones.ToArray();
                    copyRoot = null;
                }

              
            }
        }
    }
}