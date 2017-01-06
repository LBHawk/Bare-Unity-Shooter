 using UnityEngine;
 using UnityEngine.UI;
 public class scrip : MonoBehaviour
  {
    // When the game starts,
    void Awake()
    {
      // Add an Image component to this GameObject
      Image canvasElement = gameObject.AddComponent<Image>();
      // and change the Image's material, shared between all canvas elements, back to white.
      canvasElement.material.color = Color.white;
    }
  }