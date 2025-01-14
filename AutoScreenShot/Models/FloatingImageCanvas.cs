﻿using HMUI;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoScreenShot.Models
{
    public class FloatingImageCanvas : MonoBehaviour
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        public ImageView Image { get; set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public async void Init(string filePath)
        {
            this._imageFilePath = filePath;
            if (string.IsNullOrEmpty(filePath)) {
                return;
            }
            if (!File.Exists(this._imageFilePath)) {
                return;
            }
            if (!_chashedTexture.TryGetValue(filePath, out this._texture)) {
                byte[] datas = null;
                await Task.Run(() =>
                {
                    datas = File.ReadAllBytes(this._imageFilePath);
                }).ConfigureAwait(true);
                var extention = Path.GetExtension(filePath);
                var extentionType = string.Equals(extention, ".jpg") ? ImageExtention.JPEG : ImageExtention.PNG;

                var textuer = this.CreateTextuer2D(datas, extentionType);
                _chashedTexture.TryAdd(filePath, textuer);
                this._texture = textuer;
                if (datas == null) {
                    return;
                }
            }
            (this._rootCanvas.transform as RectTransform).sizeDelta = new Vector2(this._texture.width * 2, this._texture.height * 2);
            this._rootCanvas.transform.localScale = Vector3.one * 0.001f;
            var localScale = this._rootCanvas.transform.localScale;
            this._rootCanvas.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            if (this.Image == null) {
                this.Image = new GameObject("FloatingImage", typeof(ImageView)).GetComponent<ImageView>();
                this.Image.rectTransform.SetParent(this._rootCanvas.transform as RectTransform, false);
                this.Image.transform.localPosition = Vector3.zero;
                this.Image.material = this._noGlow;
            }
            this.Image.rectTransform.sizeDelta = new Vector2(this._texture.width, this._texture.height);
            this.Image.material.mainTexture = this._texture;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void Move() => this._rootCanvas.transform.position = new Vector3
                (
                this._rootCanvas.transform.position.x,
                this._rootCanvas.transform.position.y + Mathf.Sin(Time.time * 0.5f) / 1000,
                this._rootCanvas.transform.position.z
                );

        private Texture2D CreateTextuer2D(byte[] datas, ImageExtention extention)
        {
            this.GetImageSize(datas, extention, out var width, out var height);
            var result = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
            result.LoadImage(datas);
            return result;
        }

        private void GetImageSize(byte[] datas, ImageExtention extention, out int width, out int height)
        {
            width = 0;
            height = 0;

            switch (extention) {
                case ImageExtention.JPEG:
                    for (var i = 0; i < datas.Length; i++) {
                        if (datas[i] == 0xff) {
                            if (datas[i + 1] == 0xc0) {
                                height = datas[i + 5] * 256 + datas[i + 6];
                                width = datas[i + 7] * 256 + datas[i + 8];
                                break;
                            }
                        }
                    }
                    break;
                case ImageExtention.PNG:
                    var buf = new byte[8];
                    using (var ms = new MemoryStream(datas)) {
                        ms.Seek(16, SeekOrigin.Begin);
                        ms.Read(buf, 0, 8);
                    }
                    width = (buf[0] << 24) | (buf[1] << 16) | (buf[2] << 8) | buf[3];
                    height = (buf[4] << 24) | (buf[5] << 16) | (buf[6] << 8) | buf[7];
                    break;
                default:
                    break;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        private string _imageFilePath;
        private Canvas _rootCanvas;
        private Material _noGlow;
        private static readonly ConcurrentDictionary<string, Texture2D> _chashedTexture = new ConcurrentDictionary<string, Texture2D>();
        private Texture2D _texture;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // Unity methods
        public void Update() => this.Move();
        public void Awake()
        {
            if (this._noGlow == null) {
                this._noGlow = Instantiate(Resources.FindObjectsOfTypeAll<Material>().FirstOrDefault(x => x.name == "UINoGlow"));
            }
            this._rootCanvas = this.gameObject.AddComponent<Canvas>();
            this.gameObject.AddComponent<CurvedCanvasSettings>();
            this._rootCanvas.renderMode = RenderMode.WorldSpace;
        }
        #endregion
    }
}
