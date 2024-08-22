using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test6 : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Boþluklar
    [SerializeField] float topOffset = 100.0f;   // Ekranýn üstünden aþaðýya býrakmak istediðin mesafe
    [SerializeField] float bottomOffset = 50.0f; // Ekranýn altýndan yukarýya býrakmak istediðin mesafe

    // Grid boyutlarý
    [SerializeField] int gridWidth = 4;
    [SerializeField] int gridHeight = 6;
    void Start()
    {
        Camera mainCamera = Camera.main;

        // Sol üst ve sað üst köþeleri bul
        Vector3 topLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 bottomRightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        // Ekran geniþliði ve yüksekliði (dünya koordinatlarýnda)
        float screenWorldWidth = Vector3.Distance(topLeftWorld, new Vector3(bottomRightWorld.x, topLeftWorld.y, topLeftWorld.z));
        float screenWorldHeight = Vector3.Distance(topLeftWorld, new Vector3(topLeftWorld.x, bottomRightWorld.y, bottomRightWorld.z));



       

        // Kullanýlabilir yükseklik (topOffset ve bottomOffset çýkarýldý)
        float availableHeight = screenWorldHeight - topOffset - bottomOffset;

        // Hücre boyutlarýný hesapla
        float cellWidth = screenWorldWidth / gridWidth;
        float cellHeight = availableHeight / gridHeight;

        // Hücre boyutunu kare yapmak istersen minimum boyutu seç
        float cellSize = Mathf.Min(cellWidth, cellHeight);

        // Grid'in toplam yüksekliði ve geniþliði
        float gridTotalHeight = cellSize * gridHeight;
        float gridTotalWidth = cellSize * gridWidth;


        // Grid'in baþlangýç pozisyonunu hesapla (sol üst köþe)
        Vector3 gridStartPos = new Vector3(
            topLeftWorld.x + (screenWorldWidth - gridTotalWidth) / 2, // X pozisyonu: ekranýn ortasýna göre ayarlanmýþ
            topLeftWorld.y - topOffset - (availableHeight - gridTotalHeight) / 2, // Y pozisyonu: yukarýdan topOffset + merkezleme
            topLeftWorld.z
        );

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // Yeni bir hücre oluþtur
                GameObject cell = Instantiate(prefab);
                cell.name = "Cell_" + x + "_" + y;

                // Hücre boyutunu scale ile ayarla
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1f);

                // Hücrenin pozisyonunu hesapla
                float posX = x * cellSize;
                float posY = y * cellSize;

                // Hücreyi hesaplanan baþlangýç pozisyonuna göre yerleþtir
                cell.transform.position = new Vector3(gridStartPos.x + posX + cellSize / 2, gridStartPos.y - posY - cellSize / 2, gridStartPos.z);
            }
        }
    }

  
}
