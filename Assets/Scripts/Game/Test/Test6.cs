using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test6 : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Bo�luklar
    [SerializeField] float topOffset = 100.0f;   // Ekran�n �st�nden a�a��ya b�rakmak istedi�in mesafe
    [SerializeField] float bottomOffset = 50.0f; // Ekran�n alt�ndan yukar�ya b�rakmak istedi�in mesafe

    // Grid boyutlar�
    [SerializeField] int gridWidth = 4;
    [SerializeField] int gridHeight = 6;
    void Start()
    {
        Camera mainCamera = Camera.main;

        // Sol �st ve sa� �st k��eleri bul
        Vector3 topLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 bottomRightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        // Ekran geni�li�i ve y�ksekli�i (d�nya koordinatlar�nda)
        float screenWorldWidth = Vector3.Distance(topLeftWorld, new Vector3(bottomRightWorld.x, topLeftWorld.y, topLeftWorld.z));
        float screenWorldHeight = Vector3.Distance(topLeftWorld, new Vector3(topLeftWorld.x, bottomRightWorld.y, bottomRightWorld.z));



       

        // Kullan�labilir y�kseklik (topOffset ve bottomOffset ��kar�ld�)
        float availableHeight = screenWorldHeight - topOffset - bottomOffset;

        // H�cre boyutlar�n� hesapla
        float cellWidth = screenWorldWidth / gridWidth;
        float cellHeight = availableHeight / gridHeight;

        // H�cre boyutunu kare yapmak istersen minimum boyutu se�
        float cellSize = Mathf.Min(cellWidth, cellHeight);

        // Grid'in toplam y�ksekli�i ve geni�li�i
        float gridTotalHeight = cellSize * gridHeight;
        float gridTotalWidth = cellSize * gridWidth;


        // Grid'in ba�lang�� pozisyonunu hesapla (sol �st k��e)
        Vector3 gridStartPos = new Vector3(
            topLeftWorld.x + (screenWorldWidth - gridTotalWidth) / 2, // X pozisyonu: ekran�n ortas�na g�re ayarlanm��
            topLeftWorld.y - topOffset - (availableHeight - gridTotalHeight) / 2, // Y pozisyonu: yukar�dan topOffset + merkezleme
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
