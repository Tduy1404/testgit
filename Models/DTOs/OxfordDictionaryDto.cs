namespace DotnetAPIProject.Models.DTOs
{
    public class OxfordDictionaryDto
    {

        public class PhoneticDTO
        {
            public string Text { get; set; }
            public string Audio { get; set; }
        }

        public class OxfordDictionaryDTO
        {
            public string Word { get; set; }
            public string Phonetic { get; set; }
            public List<PhoneticDTO> Phonetics { get; set; }
        }
    }
    
}


    
