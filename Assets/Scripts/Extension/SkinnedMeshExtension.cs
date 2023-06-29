using UnityEngine;

namespace Extension
{
    public static class SkinnedMeshExtension
    {
        public static void SwitchRig(this SkinnedMeshRenderer skinnedMeshRenderer, Transform newRootBone)
        {
            var newBones = new Transform[skinnedMeshRenderer.bones.Length];
            for (int i = 0; i < skinnedMeshRenderer.bones.Length; i++)
            {
                foreach (var newBone in newRootBone.GetComponentsInChildren<Transform>())
                {
                    if (newBone.name == skinnedMeshRenderer.bones[i].name)
                    {
                        newBones[i] = newBone;
                    }
                }
            }
            skinnedMeshRenderer.bones = newBones;
            skinnedMeshRenderer.rootBone = newRootBone;
        }
    }
}