using System;
using System.Collections;
using UnityEngine;

namespace Svga
{
    public partial class SvgaPlayer
    {
        private int _currentFrame;

        /// <summary>
        /// 当前已播放次数.
        /// </summary>
        private int PlayedCount { get; set; }

        /// <summary>
        /// 播放循环次数, 默认为 0.
        /// 当为 0 时代表无限循环播放.
        /// </summary>
        private int LoopCount { get; set; }

        /// <summary>
        /// 是否处于播放状态
        /// </summary>
        private bool _isPlaying;

        private void DrawFrame(int currentFrame)
        {
            foreach (var shaderPropertyController in _subSpritesList)
            {
                shaderPropertyController.DrawFrame(currentFrame);
            }
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="times"></param> 播放循环次数, 默认为 0.当为 0 时代表无限循环播放.
        /// <param name="callback"></param> 播放完成回调
        public void Play(int times = 0, Action callback = null)
        {
            // 禁止在播放状态下多次调用播放
            if (_isPlaying) return;

            PlayedCount = 0;
            _isPlaying = true;

            LoopCount = times;

            _isPlaying = true;

            StartCoroutine(UpdateFrame(callback));
        }

        IEnumerator UpdateFrame(Action callback)
        {
            while (PlayedCount < LoopCount || LoopCount == 0)
            {
                if (_currentFrame > TotalFrame - 1)
                {
                    _currentFrame = 0;
                    PlayedCount += 1;

                    if (PlayedCount >= LoopCount && LoopCount != 0)
                    {
                        _isPlaying = false;
                        callback?.Invoke();
                        yield break;
                    }
                }

                DrawFrame(_currentFrame);
                _currentFrame += 1;

                if (!_isPlaying) yield break;

                if (MovieParams != null) yield return new WaitForSeconds(0.04f); // 1 / MovieParams.Fps
            }
        }

        public void Pause()
        {
            _isPlaying = false;
        }
    }
}