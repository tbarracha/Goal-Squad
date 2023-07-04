
using System;

namespace StardropTools.Tween
{
    public static class TweenEase
    {
        public static float Ease(EaseType easeType, float time)
            => Ease(easeType, 0, 1, time, 1);

        public static float Ease(EaseType easeType, float start, float end, float time, float duration = 1.0f)
        {
            switch (easeType)
            {
                // Linear
                case EaseType.Linear:
                    return Linear(time, start, end, duration);

                // Sine
                case EaseType.EaseInSine:
                    return EaseInSine(time, start, end, duration);
                case EaseType.EaseOutSine:
                    return EaseOutSine(time, start, end, duration);
                case EaseType.EaseInOutSine:
                    return EaseInOutSine(time, start, end, duration);

                // Quad
                case EaseType.EaseInQuad:
                    return EaseInQuad(time, start, end, duration);
                case EaseType.EaseOutQuad:
                    return EaseOutQuad(time, start, end, duration);
                case EaseType.EaseInOutQuad:
                    return EaseInOutQuad(time, start, end, duration);

                // Cubic
                case EaseType.EaseInCubic:
                    return EaseInCubic(time, start, end, duration);
                case EaseType.EaseOutCubic:
                    return EaseOutCubic(time, start, end, duration);
                case EaseType.EaseInOutCubic:
                    return EaseInOutCubic(time, start, end, duration);

                // Quart
                case EaseType.EaseInQuart:
                    return EaseInQuart(time, start, end, duration);
                case EaseType.EaseOutQuart:
                    return EaseOutQuart(time, start, end, duration);
                case EaseType.EaseInOutQuart:
                    return EaseInOutQuart(time, start, end, duration);

                // Quint
                case EaseType.EaseInQuint:
                    return EaseInQuint(time, start, end, duration);
                case EaseType.EaseOutQuint:
                    return EaseOutQuint(time, start, end, duration);
                case EaseType.EaseInOutQuint:
                    return EaseInOutQuint(time, start, end, duration);

                // Expo
                case EaseType.EaseInExpo:
                    return EaseInExpo(time, start, end, duration);
                case EaseType.EaseOutExpo:
                    return EaseOutExpo(time, start, end, duration);
                case EaseType.EaseInOutExpo:
                    return EaseInOutExpo(time, start, end, duration);

                // Circ
                case EaseType.EaseInCirc:
                    return EaseInCirc(time, start, end, duration);
                case EaseType.EaseOutCirc:
                    return EaseOutCirc(time, start, end, duration);
                case EaseType.EaseInOutCirc:
                    return EaseInOutCirc(time, start, end, duration);

                // Back
                case EaseType.EaseInBack:
                    return EaseInBack(time, start, end, duration);
                case EaseType.EaseOutBack:
                    return EaseOutBack(time, start, end, duration);
                case EaseType.EaseInOutBack:
                    return EaseInOutBack(time, start, end, duration);

                // Elastic
                case EaseType.EaseInElastic:
                    return EaseInElastic(time, start, end, duration);
                case EaseType.EaseOutElastic:
                    return EaseOutElastic(time, start, end, duration);
                case EaseType.EaseInOutElastic:
                    return EaseInOutElastic(time, start, end, duration);

                // Bounce
                case EaseType.EaseInBounce:
                    return EaseInBounce(time, start, end, duration);
                case EaseType.EaseOutBounce:
                    return EaseOutBounce(time, start, end, duration);
                case EaseType.EaseInOutBounce:
                    return EaseInOutBounce(time, start, end, duration);

                default:
                    return EaseInOutSine(time, start, end, duration);
            }
        }

        static float Linear(float time, float start, float end, float duration = 1.0f)
            => (duration - time) * start + time * end;


        #region Sine

        static float EaseInSine(float time, float start, float end, float duration = 1.0f)
            => -end * (float)Math.Cos(time / duration * (Math.PI / 2)) + end + start;

        static float EaseOutSine(float time, float start, float end, float duration = 1.0f)
            => end * (float)Math.Sin(time / duration * (Math.PI / 2)) + start;

        static float EaseInOutSine(float time, float start, float end, float duration = 1.0f)
            => -end * .5f * ((float)Math.Cos(Math.PI * time / duration) - 1) + start;

        #endregion // Sine


        #region Quad
        static float EaseInQuad(float time, float start, float end, float duration = 1.0f)
            => end * (time /= duration) * time + start;

        static float EaseOutQuad(float time, float start, float end, float duration = 1.0f)
            => -end * (time /= duration) * (time - 2) + start;

        static float EaseInOutQuad(float time, float start, float end, float duration = 1.0f)
        {
            if ((time /= duration / 2) < 1)
                return end * .5f * time * time + start;
            
            return -end * .5f * ((--time) * (time - 2) - 1) + start;
        }
        #endregion // Quad


        #region Quart

        static float EaseInQuart(float time, float start, float end, float duration = 1.0f)
            => end * (time /= duration) * time * time * time + start;

        static float EaseOutQuart(float time, float start, float end, float duration = 1.0f)
            => -end * ((time = time / duration - 1) * time * time * time - 1) + start;

        static float EaseInOutQuart(float time, float start, float end, float duration = 1.0f)
        {
            if ((time /= duration / 2) < 1)
                return end * .5f * time * time * time * time + start;
            
            return -end * .5f * ((time -= 2) * time * time * time - 2) + start;
        }

        #endregion // Quart


        #region Cubic
        static float EaseInCubic(float time, float start, float end, float duration = 1.0f)
            => end * (time /= duration) * time * time + start;

        static float EaseOutCubic(float time, float start, float end, float duration = 1.0f)
            => end * ((time = time / duration - 1) * time * time + 1) + start;

        static float EaseInOutCubic(float time, float start, float end, float duration = 1.0f)
        {
            if ((time /= duration / 2) < 1)
                return end * .5f * time * time * time + start;

            return end * .5f * ((time -= 2) * time * time + 2) + start;
        }
        #endregion // Cubic


        #region Quint

        static float EaseInQuint(float time, float start, float end, float duration = 1.0f)
            => end * (time /= duration) * time * time * time * time + start;

        static float EaseOutQuint(float time, float start, float end, float duration = 1.0f)
            => end * ((time = time / duration - 1) * time * time * time * time + 1) + start;

        static float EaseInOutQuint(float time, float a, float b, float lerpTotal = 1.0f)
        {
            if ((time /= lerpTotal / 2) < 1)
                return b * .5f * time * time * time * time * time + a;
            
            return b * .5f * ((time -= 2) * time * time * time * time + 2) + a;
        }

        #endregion // Quint


        #region Expo

        static float EaseInExpo(float time, float start, float end, float duration = 1.0f)
        {
            if (time == 0)
                return start;
            
            else
                return end * (float)Math.Pow(2, 10 * (time / duration - 1)) + start;
        }

        static float EaseOutExpo(float time, float start, float end, float duration = 1.0f)
        {
            if (time == duration)
                return start + end;

            else
                return end * (-(float)Math.Pow(2, -10 * time / duration) + 1) + start;
        }

        static float EaseInOutExpo(float time, float start, float end, float duration = 1.0f)
        {
            if (time == 0)
                return start;

            else if (time == duration)
                return start + end;

            if ((time /= duration / 2) < 1)
                return end * .5f * (float)Math.Pow(2, 10 * (time - 1)) + start;

            return end * .5f * (-(float)Math.Pow(2, -10 * --time) + 2) + start;
        }

        #endregion // Expo


        #region Circ

        static float EaseInCirc(float time, float start, float end, float duration = 1.0f)
            => -end * ((float)Math.Sqrt(1 - (time /= duration) * time) - 1) + start;

        static float EaseOutCirc(float time, float start, float end, float duration = 1.0f)
            => end * (float)Math.Sqrt(1 - (time = time / duration - 1) * time) + start;

        static float EaseInOutCirc(float time, float start, float end, float duration = 1.0f)
        {
            if ((time /= duration / 2) < 1)
                return -end / 2 * ((float)Math.Sqrt(1 - time * time) - 1) + start;

            return end / 2 * ((float)Math.Sqrt(1 - (time -= 2) * time) + 1) + start;
        }

        #endregion // Circ


        #region Back

        static float EaseInBack(float time, float start, float end, float duration = 1.0f, float overShoot = 1.70158f)
            => end * (time /= duration) * time * ((overShoot + 1) * time - overShoot) + start;

        static float EaseOutBack(float time, float start, float end, float duration = 1.0f, float overShoot = 1.70158f)
            => end * ((time = time / duration - 1) * time * ((overShoot + 1) * time + overShoot) + 1) + start;

        static float EaseInOutBack(float time, float start, float end, float duration = 1.0f, float overShoot = 1.70158f)
        {
            if ((time /= duration / 2) < 1)
                return end * .5f * (time * time * (((overShoot *= (1.525f)) + 1) * time - overShoot)) + start;
            
            return end * .5f * ((time -= 2) * time * (((overShoot *= (1.525f)) + 1) * time + overShoot) + 2) + start;
        }

        #endregion // Back


        #region Elastic
        static float EaseInElastic(float time, float start, float end, float duration = 1.0f)
        {
            if (time == 0)
                return start;
            
            if ((time /= duration) == 1)
                return start + end;
            
            float p = duration * .3f;
            float i = end;
            float s = p * .25f;
            float postFix = i * (float)Math.Pow(2, 10 * (time -= 1));

            return -(postFix * (float)Math.Sin((time * duration - s) * (2 * Math.PI) / p)) + start;
        }

        static float EaseOutElastic(float time, float start, float end, float duration = 1.0f)
        {
            if (time == 0)
                return start;
            
            if ((time /= duration) == 1)
                return start + end;
            
            float p = duration * .3f;
            float i = end;
            float s = p * .25f;

            return (i * (float)Math.Pow(2, -10 * time) * (float)Math.Sin((time * duration - s) * (2 * (float)Math.PI) / p) + end + start);
        }

        static float EaseInOutElastic(float time, float start, float end, float duration = 1.0f)
        {
            if (time == 0)
                return start;

            if ((time /= duration * .5f) == 2)
                return start + end;

            float p = duration * (.3f * 1.5f);
            float i = end;
            float s = p * .25f;

            if (time < 1)
                return -.5f * (i * (float)Math.Pow(2, 10 * (time -= 1)) * (float)Math.Sin((time * duration - s) * (2 * (float)Math.PI) / p)) + start;
            
            return i * (float)Math.Pow(2, -10 * (time -= 1)) * (float)Math.Sin((time * duration - s) * (2 * (float)Math.PI) / p) * .5f + end + start;
        }

        #endregion // Elastic


        #region Bounce

        static float EaseInBounce(float time, float start, float end, float duration = 1.0f)
            => end - EaseOutBounce(duration - time, 0, end, duration) + start;

        static float EaseOutBounce(float time, float start, float end, float duration = 1.0f)
        {
            if ((time /= duration) < (1 / 2.75f))
                return end * (7.5625f * time * time) + start;

            else if (time < (2 / 2.75f))
                return end * (7.5625f * (time -= (1.5f / 2.75f)) * time + .75f) + start;

            else if (time < (2.5 / 2.75))
                return end * (7.5625f * (time -= (2.25f / 2.75f)) * time + .9375f) + start;

            else
                return end * (7.5625f * (time -= (2.625f / 2.75f)) * time + .984375f) + start;
        }

        static float EaseInOutBounce(float time, float start, float end, float duration = 1.0f)
        {
            if (time < duration / 2)
                return EaseInBounce(time * 2, 0, end, duration) * .5f + start;

            else
                return EaseOutBounce(time * 2 - duration, 0, end, duration) * .5f + end * .5f + start;
        }
        #endregion // Bounce
    }
}