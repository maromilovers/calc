using Android.Media;
using calc;
using calc.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Sound01.Droid.SoundEffect))]
namespace Sound01.Droid
{
    public class SoundEffect : ISoundEffect
    {
        SoundPool soundPool;
        int HitId;
        int CorrectId;
        int IncorrectId;
        int ResultId;

        public SoundEffect()
        {
            int SOUND_POOL_MAX = 6;

            AudioAttributes attr = new AudioAttributes.Builder()
                .SetUsage(AudioUsageKind.Media)
                .SetContentType(AudioContentType.Music)
                .Build();
            soundPool = new SoundPool.Builder()
               .SetAudioAttributes(attr)
               .SetMaxStreams(SOUND_POOL_MAX)
               .Build();
            HitId = soundPool.Load(Android.App.Application.Context, Resource.Raw.touch, 1);
            CorrectId = soundPool.Load(Android.App.Application.Context, Resource.Raw.maru, 1);
            IncorrectId = soundPool.Load(Android.App.Application.Context, Resource.Raw.batu, 1);
            ResultId = soundPool.Load(Android.App.Application.Context, Resource.Raw.result, 1);

        }

        public void HitSound()
        {
            soundPool.Play(HitId, 1.0F, 1.0F, 0, 0, 1.0F);
        }

        public void CorrectSound()
        {
            soundPool.Play(CorrectId, 1.0F, 1.0F, 0, 0, 1.0F);
        }

        public void IncorrectSound()
        {
            soundPool.Play(IncorrectId, 1.0F, 1.0F, 0, 0, 1.0F);
        }
        public void ResultSound()
        {
            soundPool.Play(ResultId, 1.0F, 1.0F, 0, 0, 1.0F);
        }
    }
}