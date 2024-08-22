using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int gridWidth = 4;
    [SerializeField] int gridHeight = 6;
    void Start()
    {
        // Ana kamerayý al
        Camera mainCamera = Camera.main;

        // Sol üst ve sað üst köþeleri bul
        Vector3 topLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 topRightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Vector3 bottomLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));

        // Ekran geniþliði ve yüksekliði (dünya koordinatlarýnda)
        float screenWorldWidth = Vector3.Distance(topLeftWorld, topRightWorld);
        float screenWorldHeight = Vector3.Distance(topLeftWorld, bottomLeftWorld);

        // Grid boyutlarý (örneðin 4x6)
    

        // Hücre boyutlarýný geniþlik ve yükseklik için hesapla
        float cellWidth = screenWorldWidth / gridWidth;
        float cellHeight = screenWorldHeight / gridHeight;

        // Hücre boyutunu kare yapmak istersen minimum boyutu seç
        float cellSize = Mathf.Min(cellWidth, cellHeight);

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

                // Hücreyi sol üst köþeye göre yerleþtir
                cell.transform.position = new Vector3(topLeftWorld.x + posX + cellSize / 2, topLeftWorld.y - posY - cellSize / 2, topLeftWorld.z);
            }
        }
    }

  
}
