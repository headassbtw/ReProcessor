﻿using BeatSaberMarkupLanguage.Components;
using IPA.Loader;
using SiraUtil.Zenject;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRUIControls;
using Zenject;

namespace ReProcessor.UI
{
    internal class ButtonManager : IInitializable, IDisposable
    {
        private ClickableImage? _image;
        public event Action? WasClicked;
        private readonly Assembly _assembly;
        private readonly DiContainer _container;
        private readonly TweeningManager _tweeningManager;
        private readonly LevelSelectionNavigationController _levelSelectionNavigationController;
        private static readonly Color _emptyColor = new Color(0.15f, 0f, 0f, 1f);

        public Color? DefaultColor
        {
            get
            {
                if (_image != null)
                {
                    return _image.DefaultColor;
                }
                return null;
            }
            set
            {
                if (_image != null && value.HasValue)
                {
                    Color oldColor = _image.color;
                    _tweeningManager.KillAllTweens(_image);
                    var tween = new FloatTween(0f, 1f, val => _image!.color = Color.Lerp(oldColor, value.Value, val), 1f, EaseType.InOutSine);
                    _tweeningManager.AddTween(tween, _image);
                    tween.onCompleted = delegate () { _image!.DefaultColor = value.Value; };
                }
            }
        }

        public ButtonManager(DiContainer container, UBinder<Plugin, PluginMetadata> metadataBinder, TweeningManager tweeningManager, LevelSelectionNavigationController levelSelectionNavigationController)
        {
            _container = container;
            _tweeningManager = tweeningManager;
            _assembly = metadataBinder.Value.Assembly;
            _levelSelectionNavigationController = levelSelectionNavigationController;
        }

        public void Initialize()
        {
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            _image = CreateImage();
            using Stream mrs = _assembly.GetManifestResourceStream("ReProcessor.UI.a.png");
            using MemoryStream ms = new MemoryStream();
            await mrs.CopyToAsync(ms);

            _image.OnClickEvent += Clicked;
            _image.sprite = BeatSaberMarkupLanguage.Utilities.LoadSpriteRaw(ms.ToArray());
            _image.sprite.texture.wrapMode = TextureWrapMode.Clamp;
        }

        public void Dispose()
        {
            if (_image != null)
                _image.OnClickEvent -= Clicked;
        }

        private void Clicked(PointerEventData _)
        {
            WasClicked?.Invoke();
        }

        private ClickableImage CreateImage()
        {
            GameObject gameObject = new GameObject("ReProcessorButton");
            ClickableImage image = gameObject.AddComponent<ClickableImage>();
            image.material = BeatSaberMarkupLanguage.Utilities.ImageResources.NoGlowMat;

            image.rectTransform.SetParent(_levelSelectionNavigationController.transform);
            image.rectTransform.localPosition = new Vector3(79f, 18f, 0f);
            image.rectTransform.localScale = new Vector3(.3f, .3f, .3f);
            image.rectTransform.sizeDelta = new Vector2(20f, 20f);
            gameObject.AddComponent<LayoutElement>();

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Tangent;
            canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Normal;
            _container.InstantiateComponent<VRGraphicRaycaster>(gameObject);

            return image;
        }
    }
}