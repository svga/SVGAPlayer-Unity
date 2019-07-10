using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Svga
{
    public partial class SvgaPlayer : MonoBehaviour
    {
        /// <summary>
        /// 舞台是否已经初始化.
        /// </summary>
        private bool _isStageInited;

        private readonly List<ShaderPropertyController> _subSpritesList =
            new List<ShaderPropertyController>();

        public Material m;

        private void InitialView()
        {
            var viewBox = new Vector2(MovieParams.ViewBoxWidth, MovieParams.ViewBoxHeight);
            GetComponent<RectTransform>().sizeDelta = viewBox;
            var sprites = Sprites;

            for (int i = 0; i < sprites.Count; i++)
            {
                // 创建SpriteEntity实体
                var SpriteObject = new GameObject(sprites[i].ImageKey, typeof(Image));
                SpriteObject.GetComponent<Image>().material = new Material(m);
                SpriteObject.AddComponent<ShaderPropertyController>();
                SpriteObject.transform.SetParent(transform);

                // 赋 Sprite 贴图
                var img = SpriteObject.GetComponent<Image>();
                img.sprite = SpriteImages[sprites[i].ImageKey];

                // 设置初始布局
                var rect = SpriteObject.GetComponent<RectTransform>();

                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);

                rect.pivot = new Vector2(0, 1);
                rect.anchoredPosition = Vector2.zero;
                rect.localScale = Vector3.one;
                rect.sizeDelta = new Vector2(img.sprite.texture.width,
                    img.sprite.texture.height);

                // 捆绑数据
                var frameList = new List<FrameEntity>();
                //Debug.Log(sprites);
                foreach (var frame in sprites[i].Frames)
                {
                    frameList.Add(frame);
                }

                var spc = SpriteObject.GetComponent<ShaderPropertyController>();
                spc.frameEntities = frameList;
                _subSpritesList.Add(spc);

                _isStageInited = true;
            }
        }
    }
}