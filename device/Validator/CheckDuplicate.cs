using device.Cons;

namespace device.Validator
{
    public class CheckDuplicate
    {
        public bool isValueName ( string name)
        {
            int repeatStr = CountCharacterDuplicate(name.ToLower());
            return repeatStr <= Constants.MAX_REPEAT_NAME;
        }

        public int CountCharacterDuplicate (string str)
        {
            int maxCountRepeat = 0;
            int currentCountRepeat = 1;
            for (int i =1 ; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    currentCountRepeat++;
                }
                else
                {
                    maxCountRepeat = Math.Max(maxCountRepeat, currentCountRepeat);
                    currentCountRepeat = 1;
                }
            }
            maxCountRepeat = Math.Max (maxCountRepeat, currentCountRepeat);
            return maxCountRepeat;
        }
    }
}
