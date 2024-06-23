using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapLogic : MonoBehaviour
{
    public GameObject playerCar;
    public GameObject track;
    public GameObject miniMap;
    public GameObject trackerDot;

    SpriteRenderer trackSprite;
    RectTransform miniMapImage, trackerDotImage;

    Vector2 trackPos, trackSize, miniMapPos, miniMapSize;
    float xScale, yScale;

    // Start is called before the first frame update
    void Start()
    {
        trackSprite = track.GetComponent<SpriteRenderer>();
        miniMapImage = miniMap.GetComponent<RectTransform>();
        trackerDotImage = trackerDot.GetComponent<RectTransform>();

        // Get position of track
        trackPos = track.transform.position;
        // Get size of track (x and y directions)
        trackSize = trackSprite.size * track.transform.localScale;

        // Get position of minimap
        miniMapPos = miniMapImage.position;
        // Get size of minimap (x + y)
        miniMapSize = new Vector2 (miniMapImage.localScale.x * miniMapImage.rect.height, miniMapImage.localScale.y * miniMapImage.rect.width);

        // Calculate rescaling factor between track and minimap
        xScale = miniMapSize.x / trackSize.x;
        yScale = miniMapSize.y / trackSize.y;
    }

    void FixedUpdate()
    {
        // Get position of car
        Vector2 carPos = playerCar.transform.position;
        // Normalize car coords relative to track center
        Vector2 carRelPos = carPos - trackPos;

        // Rescale relative car position to minimap coords
        float rescaleX = carRelPos.x * xScale;
        float rescaleY = carRelPos.y * yScale;
        Vector2 trackerPos = new Vector2(rescaleX, rescaleY);

        // Update position of tracker dot on screen
        trackerPos += miniMapPos;
        trackerDotImage.position = trackerPos;
    }

}
