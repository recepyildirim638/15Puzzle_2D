using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4 : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Bo�luklar
    [SerializeField] float margin = 1.0f; // D�nya birimlerinde �rne�in 1 birimlik bo�luk

    // Grid boyutlar�
    [SerializeField] int gridWidth = 4;
    [SerializeField] int gridHeight = 6;
    void Start()
    {
        // Ana kameray� al
        Camera mainCamera = Camera.main;

        // Sol �st ve sa� �st k��eleri bul
        Vector3 topLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 bottomRightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        // Ekran geni�li�i ve y�ksekli�i (d�nya koordinatlar�nda)
        float screenWorldWidth = Vector3.Distance(topLeftWorld, new Vector3(bottomRightWorld.x, topLeftWorld.y, topLeftWorld.z));
        float screenWorldHeight = Vector3.Distance(topLeftWorld, new Vector3(topLeftWorld.x, bottomRightWorld.y, bottomRightWorld.z));


       

        // Kullan�labilir geni�lik ve y�kseklik (bo�luklar ��kar�ld�)
        float availableWidth = screenWorldWidth - 2 * margin;
        float availableHeight = screenWorldHeight - 2 * margin;

        // H�cre boyutlar�n� hesapla
        float cellWidth = availableWidth / gridWidth;
        float cellHeight = availableHeight / gridHeight;

        // H�cre boyutunu kare yapmak istersen minimum boyutu se�
        float cellSize = Mathf.Min(cellWidth, cellHeight);


        // Grid'in toplam geni�li�i ve y�ksekli�i
        float gridTotalWidth = cellSize * gridWidth;
        float gridTotalHeight = cellSize * gridHeight;

        // Grid'in ba�lang�� pozisyonunu hesapla (sol �st k��e)
        Vector3 gridStartPos = new Vector3(
            topLeftWorld.x + (screenWorldWidth - gridTotalWidth) / 2 + margin,
            topLeftWorld.y - (screenWorldHeight - gridTotalHeight) / 2 - margin,
            topLeftWorld.z
        );

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

                // H�creyi hesaplanan ba�lang�� pozisyonuna g�re yerle�tir
                cell.transform.position = new Vector3(gridStartPos.x + posX + cellSize / 2, gridStartPos.y - posY - cellSize / 2, gridStartPos.z);
            }
        }
    }

   
}
