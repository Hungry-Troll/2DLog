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
            Debug.Log("선택이 취소되었습니다.");
            return;
        }

        // 절대 경로 → Unity 상대 경로로 변환
        string relativePath = "Assets" + fullPath.Substring(Application.dataPath.Length);
        TextureImporter ti = AssetImporter.GetAtPath(relativePath) as TextureImporter;

        if (ti == null)
        {
            Debug.LogError("TextureImporter not found");
            return;
        }

        if (ti.spriteImportMode != SpriteImportMode.Multiple)
        {
            Debug.LogError("이 텍스처는 Multiple 모드가 아닙니다 (슬라이스되지 않음)");
            return;
        }
        // 파일명 추출 (확장자 제거)
        string textureName = Path.GetFileNameWithoutExtension(relativePath);

        // 슬라이스 데이터 가져오기
        SpriteMetaData[] metas = ti.spritesheet;

        for (int i = 0; i < metas.Length; i++)
        {
            string newName = $"{textureName}_{i.ToString("D4")}";
            metas[i].name = newName;
            Debug.Log($"이름 변경: {newName}");
        }

        // 수정한 데이터 다시 설정
        ti.spritesheet = metas;

        // 저장
        EditorUtility.SetDirty(ti);
        ti.SaveAndReimport();
    }
}

