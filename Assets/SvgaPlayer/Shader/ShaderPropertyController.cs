using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Svga;

public class ShaderPropertyController : MonoBehaviour
{
    public List<FrameEntity> frameEntities;

    //[TableMatrix(HorizontalTitle = "3x2 Matrix")]
    private float[] _matrix32 = new float[6];

    private float _alpha;

    private SvgaPlayer _svgaPlayer;
    private Vector2 _viewBoxRect;

    private void Start()
    {
        _svgaPlayer = GetComponentInParent<SvgaPlayer>();
        _viewBoxRect = _svgaPlayer.GetComponent<RectTransform>().sizeDelta;
    }

    private void UpdateMatrix32()
    {
        var pos = transform.position;
        var transformMatrix = new Matrix4x4(
            new Vector4(_matrix32[0], _matrix32[2], 0, 0),
            new Vector4(_matrix32[1], _matrix32[3], 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(_matrix32[4], _matrix32[5], 0, 1)
        );

        var offsetMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(-pos.x * 100, -pos.y * 100, 0, 1)
        );
        var offsetMatrix2 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, -2 * _matrix32[5], 0, 1)
        );
        var offsetMatrix3 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(pos.x * 100, pos.y * 100, 0, 1)
        );
        var MixMatrix = offsetMatrix3 * offsetMatrix2 * transformMatrix * offsetMatrix;

        GetComponent<Image>().material.SetMatrix("_Matrix32", MixMatrix);
        GetComponent<Image>().color = new Color(1,1,1,_alpha);
    }

    public void DrawFrame(int currentFrame)
    {
        _alpha = frameEntities[currentFrame].Alpha;
        var m = frameEntities[currentFrame].Transform;
        _matrix32 = m != null ? new[] {m.A, m.B, m.C, m.D, m.Tx, m.Ty} : new[] {1f, 0, 0, 1f, 0, 0};

        UpdateMatrix32();
    }
}