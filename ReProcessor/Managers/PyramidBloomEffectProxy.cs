using IPA.Utilities;
using ReProcessor.Configuration;

namespace ReProcessor.Managers
{
    internal class PyramidBloomEffectProxy : ICameraSettings
    {
        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor BloomRadiusAccessor = FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_bloomRadius");
        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor BlendFactorAccessor = FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_bloomBlendFactor");
        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor IntensityAccessor = FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_bloomIntensity");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor IntensityOffsetAccessor =
            FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_downBloomIntensityOffset");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor WeightAccessor = FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_pyramidWeightsParam");
        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor AlphaWeightsAccessor = FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_alphaWeights");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.Accessor PreFilterPassAccessor =
            FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.GetAccessor("_preFilterPass");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.Accessor DownSamplePassAccessor =
            FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.GetAccessor("_downsamplePass");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.Accessor UpSamplePassAccessor =
            FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.GetAccessor("_upsamplePass");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.Accessor FinalUpSamplePassAccessor =
            FieldAccessor<PyramidBloomMainEffectSO, PyramidBloomRendererSO.Pass>.GetAccessor("_finalUpsamplePass");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor BaseColorBoostAccessor = FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_baseColorBoost");

        private static readonly FieldAccessor<PyramidBloomMainEffectSO, float>.Accessor BaseColorBoostThresholdAccessor =
            FieldAccessor<PyramidBloomMainEffectSO, float>.GetAccessor("_baseColorBoostThreshold");

        private PyramidBloomMainEffectSO _pyramidBloomMainEffectSo;

        public PyramidBloomEffectProxy(PyramidBloomMainEffectSO pyramidBloomMainEffectSo)
        {
            _pyramidBloomMainEffectSo = pyramidBloomMainEffectSo;
        }

        public float BloomRadius
        {
            get => BloomRadiusAccessor(ref _pyramidBloomMainEffectSo);
            set => BloomRadiusAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float BlendFactor
        {
            get => BlendFactorAccessor(ref _pyramidBloomMainEffectSo);
            set => BlendFactorAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float Intensity
        {
            get => IntensityAccessor(ref _pyramidBloomMainEffectSo);
            set => IntensityAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float IntensityOffset
        {
            get => IntensityOffsetAccessor(ref _pyramidBloomMainEffectSo);
            set => IntensityOffsetAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float Weight
        {
            get => WeightAccessor(ref _pyramidBloomMainEffectSo);
            set => WeightAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float AlphaWeights
        {
            get => AlphaWeightsAccessor(ref _pyramidBloomMainEffectSo);
            set => AlphaWeightsAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public PyramidBloomRendererSO.Pass PreFilterPass
        {
            get => PreFilterPassAccessor(ref _pyramidBloomMainEffectSo);
            set => PreFilterPassAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public PyramidBloomRendererSO.Pass DownSamplePass
        {
            get => DownSamplePassAccessor(ref _pyramidBloomMainEffectSo);
            set => DownSamplePassAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public PyramidBloomRendererSO.Pass UpSamplePass
        {
            get => UpSamplePassAccessor(ref _pyramidBloomMainEffectSo);
            set => UpSamplePassAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public PyramidBloomRendererSO.Pass FinalUpSamplePass
        {
            get => FinalUpSamplePassAccessor(ref _pyramidBloomMainEffectSo);
            set => FinalUpSamplePassAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float BaseColorBoost
        {
            get => BaseColorBoostAccessor(ref _pyramidBloomMainEffectSo);
            set => BaseColorBoostAccessor(ref _pyramidBloomMainEffectSo) = value;
        }

        public float BaseColorBoostThreshold
        {
            get => BaseColorBoostThresholdAccessor(ref _pyramidBloomMainEffectSo);
            set => BaseColorBoostThresholdAccessor(ref _pyramidBloomMainEffectSo) = value;
        }
    }
}