using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Creator
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] float topOffset = 100.0f;
        [SerializeField] float bottomOffset = 50.0f;
        [SerializeField] float leftOffset = 30.0f;
        [SerializeField] float rightOffset = 30.0f;


        List<Vector3> grids = new List<Vector3>();
        float gridScale = 1.0f;

        public float GetGridScale() => gridScale;

        public List<Vector3> CreateGrid(int width, int height)
        {
            int gridWidth = width;
            int gridHeight = height;

            grids.Clear();
            Camera mainCamera = Camera.main;

            // Sol üst ve sað üst köþeleri bul
            Vector3 topLeftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
            Vector3 bottomRightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

            // Ekran geniþliði ve yüksekliði (dünya koordinatlarýnda)
            float screenWorldWidth = Vector3.Distance(topLeftWorld, new Vector3(bottomRightWorld.x, topLeftWorld.y, topLeftWorld.z));
            float screenWorldHeight = Vector3.Distance(topLeftWorld, new Vector3(topLeftWorld.x, bottomRightWorld.y, bottomRightWorld.z));


            // Kullanýlabilir geniþlik ve yükseklik (kenarlardan çýkarýlan boþluklar)
            float availableWidth = screenWorldWidth - leftOffset - rightOffset;
            float availableHeight = screenWorldHeight - topOffset - bottomOffset;

            // Hücre boyutlarýný hesapla
            float cellWidth = availableWidth / gridWidth;
            float cellHeight = availableHeight / gridHeight;

            // Hücre boyutunu kare yapmak istersen minimum boyutu seç
            float cellSize = Mathf.Min(cellWidth, cellHeight);

            // Grid'in toplam geniþliði ve yüksekliði
            float gridTotalWidth = cellSize * gridWidth;
            float gridTotalHeight = cellSize * gridHeight;


            Vector3 gridStartPos = new Vector3(
                topLeftWorld.x + leftOffset + (availableWidth - gridTotalWidth) / 2, // X pozisyonu: soldan leftOffset + merkezleme
                topLeftWorld.y - topOffset - (availableHeight - gridTotalHeight) / 2, // Y pozisyonu: yukarýdan topOffset + merkezleme
                topLeftWorld.z
            );

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    float posX = x * cellSize;
                    float posY = y * cellSize;

                    gridScale = cellSize;
                    Vector3 gridPos = new Vector3(gridStartPos.x + posX + cellSize / 2, gridStartPos.y - posY - cellSize / 2, gridStartPos.z + 1f);
                    grids.Add(gridPos);
                }
            }
            return grids;
        }
    }
}
