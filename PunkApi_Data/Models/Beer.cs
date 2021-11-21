using Microsoft.EntityFrameworkCore;
using PunkApi_Data.Models;
using System.Text.Json.Serialization;

public class Beer
    {

      
       
        public int BeerId { get; set; }
        [JsonPropertyName("id")]
        public int ApiId { get; set; } 

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("tagline")]
        public string? Tagline { get; set; }

        [JsonPropertyName("first_brewed")]
        public string? FirstBrewed { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("abv")]
        public double Abv { get; set; }

        [JsonPropertyName("ibu")]
        public int Ibu { get; set; }

        [JsonPropertyName("target_fg")]
        public int TargetFg { get; set; }

        [JsonPropertyName("target_og")]
        public int TargetOg { get; set; }

        [JsonPropertyName("ebc")]
        public int Ebc { get; set; }

        [JsonPropertyName("srm")]
        public int Srm { get; set; }

        [JsonPropertyName("ph")]
        public double Ph { get; set; }

        [JsonPropertyName("attenuation_level")]
        public double AttenuationLevel { get; set; }

        //[JsonPropertyName("volume")]
        //public Volume? Volume { get; set; }

        //[JsonPropertyName("boil_volume")]
        //public BoilVolume? BoilVolume { get; set; }

        //[JsonPropertyName("method")]
        //public Method? Method { get; set; }

        //[JsonPropertyName("ingredients")]
        //public Ingredients? Ingredients { get; set; }

        //[JsonPropertyName("food_pairing")]
        //public List<string>? FoodPairing { get; set; }

        [JsonPropertyName("brewers_tips")]
        public string? BrewersTips { get; set; }

        [JsonPropertyName("contributed_by")]
        public string? ContributedBy { get; set; }

    [JsonIgnore]
    public virtual IList<UserBeers>? UserBeers { get; set;}

       
    }
