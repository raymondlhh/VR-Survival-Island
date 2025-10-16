using UnityEngine;

namespace FronkonGames.SpiceUp.Stoned
{
  /// <summary> Potion. </summary>
  /// <remarks> This code is designed for a simple demo, not for production environments. </remarks>
  public class Potion : MonoBehaviour
  {
    public enum Effects
    {
      One,
      Two,
      Three,
      Four,
      Five
    }

    public Effects Effect => effect;

    public Vector3 OriginalPosition { get; private set; }
    
    public Quaternion OriginalRotation { get; private set; }

    [SerializeField]
    private Effects effect;

    private Material cork;

    public void Cork(bool on) => cork.color = new Color(cork.color.r, cork.color.g, cork.color.b, on == true ? 1.0f : 0.0f);

    private void Awake()
    {
      OriginalPosition = this.gameObject.transform.position;
      OriginalRotation = this.gameObject.transform.rotation;

      Renderer renderer = this.gameObject.GetComponent<Renderer>();
      cork = Instantiate(renderer.sharedMaterials[1]);
      Material[] sharedMaterials = renderer.sharedMaterials;
      sharedMaterials[1] = cork;
      renderer.sharedMaterials = sharedMaterials;
    }
  }
}