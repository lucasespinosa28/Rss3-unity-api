using System;
using System.Collections.Generic;

namespace RSS3
{
    public class Models
    {
        public class Platform
        {

            public int total;
            public List<Result> result;

            [Serializable]
            public class Result
            {
                public string name;
                public string tag;
                public string type;
                public string network;
            }
        }

        public class Profile
        {
            public int total;
            public Result[] result;

            [Serializable]
            public class Result
            {
                public string address;
                public string network;
                public string platform;
                public string source;
                public string name;
                public string handle;
                public string bio;
                public string url;
                public string[] profile_uri;
                public DateTime expire_at;
                public string[] social_uri;
            }
        }

        public class Note
        {
            public int total;
            public Result[] result;
            public Address_Status[] address_status;
            
            [Serializable]
            public class Result
            {
                public DateTime timestamp;
                public string hash;
                public string owner;
                public string fee;
                public string address_from;
                public string address_to;
                public string network;
                public string tag;
                public string type;
                public bool success;
                public Action[] actions;
                public string platform;
            }
            [Serializable]
            public class Action
            {
                public string tag;
                public string type;
                public int index;
                public string address_from;
                public string address_to;
                public Metadata metadata;
                public string[] related_urls;
                public string platform;
            }
            [Serializable]
            public class Metadata
            {
                public string id;
                public string name;
                public string image;
                public string value;
                public string symbol;
                public string standard;
                public Attribute[] attributes;
                public string description;
                public string animation_url;
                public string value_display;
                public string contract_address;
                public string collection;
                public int decimals;
                public string body;
                public string[] author;
                public Target target;
                public string[] type_on_platform;
                public string summary;
                public string bio;
                public string type;
                public string handle;
                public string source;
                public string address;
                public string network;
                public string platform;
                public string[] profile_uri;
                public string title;
                public Medium[] media;
                public string url;
                public Cost cost;
                public string choice;
                public Proposal proposal;
                public string[] social_uri;
                public string external_link;
                public To to;
                public From from;
                public string protocol;
                public string logo;
                public Token token;
            }
            [Serializable]
            public class Target
            {
                public string body;
                public string[] author;
                public string target_url;
                public string[] type_on_platform;
            }
            [Serializable]
            public class Cost
            {
                public string name;
                public string image;
                public string value;
                public string symbol;
                public int decimals;
                public string standard;
                public string value_display;
            }
            [Serializable]
            public class Proposal
            {
                public string id;
                public string body;
                public string title;
                public DateTime end_at;
                public string[] options;
                public DateTime start_at;
                public Organization organization;
            }
            [Serializable]
            public class Organization
            {
                public string id;
                public string name;
            }
            [Serializable]
            public class To
            {
                public string name;
                public string image;
                public string value;
                public string symbol;
                public int decimals;
                public string standard;
                public string value_display;
                public string contract_address;
            }
            [Serializable]
            public class From
            {
                public string name;
                public string image;
                public string value;
                public string symbol;
                public int decimals;
                public string standard;
                public string value_display;
                public string contract_address;
            }
            [Serializable]
            public class Token
            {
                public string name;
                public string image;
                public string value;
                public string symbol;
                public int decimals;
                public string standard;
                public string value_display;
                public string contract_address;
            }
            [Serializable]
            public class Attribute
            {
                public object value;
                public string trait_type;
            }
            [Serializable]
            public class Medium
            {
                public string address;
                public string mime_type;
            }
            [Serializable]
            public class Address_Status
            {
                public string address;
                public bool status;
                public string[] done_networks;
                public object indexing_networks;
                public DateTime updated_at;
                public int count;
            }

        }

    }
}