using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteRenameEditor
{
    [MenuItem("Tools/Rename Sliced Sprites %#a")]
    public static void RenameSlicedSprites()
    {
        string fullPath = EditorUtility.OpenFilePanel("Select a Sprite Sheet", "Assets", "png");

        if (string.IsNullOrEmpty(fullPath))
        {
            Debug.Log("������ ��ҵǾ����ϴ�.");
            return;
        }

        // ���� ��� �� Unity ��� ��η� ��ȯ
        string relativePath = "Assets" + fullPath.Substring(Application.dataPath.Length);
        TextureImporter ti = AssetImporter.GetAtPath(relativePath) as TextureImporter;

        if (ti == null)
        {
            Debug.LogError("TextureImporter not found");
            return;
        }

        if (ti.spriteImportMode != SpriteImportMode.Multiple)
        {
            Debug.LogError("�� �ؽ�ó�� Multiple ��尡 �ƴմϴ� (�����̽����� ����)");
            return;
        }
        // ���ϸ� ���� (Ȯ���� ����)
        string textureName = Path.GetFileNameWithoutExtension(relativePath);

        // �����̽� ������ ��������
        SpriteMetaData[] metas = ti.spritesheet;

        for (int i = 0; i < metas.Length; i++)
        {
            string newName = $"{textureName}_{i.ToString("D4")}";
            metas[i].name = newName;
            Debug.Log($"�̸� ����: {newName}");
        }

        // ������ ������ �ٽ� ����
        ti.spritesheet = metas;

        // ����
        EditorUtility.SetDirty(ti);
        ti.SaveAndReimport();
    }
}

