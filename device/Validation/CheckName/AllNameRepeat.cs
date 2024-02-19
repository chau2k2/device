using device.Cons;

namespace device.Validation.CheckName
{
    public class AllNameRepeat
    {
        public bool IsValueName(string name)
        {
            int RepeatStr = countCharacterRepeat(name.ToLower());
            return RepeatStr <= Constants.MAX_REPEAT_NAME;
        }
        public int countCharacterRepeat(string str)
        {
            int maxCountRepeat = 0;
            int currentCountRepeats = 1;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    currentCountRepeats++;
                }
                else
                {
                    maxCountRepeat = Math.Max(maxCountRepeat, currentCountRepeats);
                    currentCountRepeats = 1;
                }
            }
            maxCountRepeat = Math.Max(maxCountRepeat, currentCountRepeats);
            return maxCountRepeat;
        }
    }
}
