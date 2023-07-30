using JetBrains.Annotations;
using UnityEngine;

public class Chalk : MonoBehaviour
{
    [SerializeField] private DrawController _drawController;
    [SerializeField] private Color _color;

    [UsedImplicitly]
    public void OnClick()
    {
        _drawController.ChooseColor(_color);
    }
}
