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
        // Ana kameray� al
        Camera mainCamera = Camera.main;

        // Sol �st ve sa� �st k��eleri bul
        Vector3 topLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 topRightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Vector3 bottomLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));

        // Ekran geni�li�i ve y�ksekli�i (d�nya koordinatlar�nda)
        float screenWorldWidth = Vector3.Distance(topLeftWorld, topRightWorld);
        float screenWorldHeight = Vector3.Distance(topLeftWorld, bottomLeftWorld);

        // Grid boyutlar� (�rne�in 4x6)
    

        // H�cre boyutlar�n� geni�lik ve y�kseklik i�in hesapla
        float cellWidth = screenWorldWidth / gridWidth;
        float cellHeight = screenWorldHeight / gridHeight;

        // H�cre boyutunu kare yapmak istersen minimum boyutu se�
        float cellSize = Mathf.Min(cellWidth, cellHeight);

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // Yeni bir h�cre olu�tur
                GameObject cell = Instantiate(prefab);
                cell.name = "Cell_" + x + "_" + y;

                // H�cre boyutunu scale ile ayarla
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1f);

                // H�crenin pozisyonunu hesapla
                float posX = x * cellSize;
                float posY = y * cellSize;

                // H�creyi sol �st k��eye g�re yerle�tir
                cell.transform.position = new Vector3(topLeftWorld.x + posX + cellSize / 2, topLeftWorld.y - posY - cellSize / 2, topLeftWorld.z);
            }
        }
    }

  
}
