using CoreInvestmentTracker.Models.DEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CoreInvestmentTracker.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace CoreInvestmentTracker.Models.DAL
{
    /// <summary>
    /// All database initilizers will have this interface
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Initialize the Initializer
        /// </summary>
        /// <param name="db"></param>
        void Initialize(InvestmentDbContext db);
    }

    /// <summary>
    /// This class is setup in the web.config to create new data in the database at startup.
    /// Its mentioned in the web.config
    /// </summary>
    public class DatabaseTestDataInitializer
    {
        /// <summary>
        /// This method(seed) basically creates and adds our entities to our derived DbContext object,
        /// which has a list(DbSet) for each of the entities we want to persist.
        /// We basically create those entities here by hand and add them to that list in the DbContext.
        /// We then ask the DBContext to SaveChanges() which persists what we've setup to be added to those lists(managed by the DbContext)
        /// </summary>
        /// <param name="db"></param>
        public static void Initialize(InvestmentDbContext db)
        {            
            db.Database.Migrate(); // apply migration at runtime, replaces db.Database.EnsureCreated();
           
            var inits = new List<IDbInitializer>(new IDbInitializer[]
            {
                new InvestmentInitilizer(), // Create test investments
                new CustomEntitiesInitializer(),   // create test custom types & Entities
                // new AdditionalDbInitializer(), // old generic investments are less useful now
            });

            // Initialize each initializer
            inits.ForEach(initializer => initializer.Initialize(db));
        }
    }

    public class InvestmentPackage
    {
        public Investment Investment { get; }
        public InvestmentInfluenceFactor[] Factors { get; }
        public InvestmentGroup[] Groups { get; }
        public InvestmentRisk[] Risks { get; }
        public InvestmentNote[] Notes { get; }
        public CustomEntity[] CustomEntities { get; }

        public InvestmentPackage(InvestmentNote[] notes, Investment investment, InvestmentInfluenceFactor[] factors, InvestmentGroup[] groups, InvestmentRisk[] risks, CustomEntity[] customEntities)
        {
            Notes = notes;
            Investment = investment;
            Factors = factors;
            Groups = groups;
            Risks = risks;
            CustomEntities = customEntities;
        }
    }

    /// <summary>
    /// Seeder for creating investment sample data
    /// </summary>
    public class InvestmentInitilizer : IDbInitializer
    {
        /// <summary>
        /// Initialize Investment Seeder
        /// </summary>
        /// <param name="db"></param>
        public void Initialize(InvestmentDbContext db)
        {
            if (db.Investments.Any())
            {
                return;   // DB has been seeded
            }

            // Setup default user - make the default system user
            User systemUser = new User
            {
                Id = 0,
                DisplayName = "System User",
                TimeZone = 0,
                Password = "",
                UserName = "system",
            };

            db.Users.Add(systemUser);
            db.SaveChanges();
            
            // Setup a default set of Investment Groups

            var property = new InvestmentGroup { Name="Property",Description="Property"};
            var chips  = new InvestmentGroup { Name="Chips ",Description="Chips "};
            var internetUtiltiy = new InvestmentGroup { Name="Internet/Utiltiy",Description="Internet/Utiltiy"};
            var aiAdvertising = new InvestmentGroup { Name="AI/Advertising/Marketing",Description="AI/Advertising"};
            var semiconductor = new InvestmentGroup { Name="Semiconductors",Description="Semiconductors"};
            var webAdvertising = new InvestmentGroup { Name="Web/Advertising",Description="Web/Advertising"};
            var cosmetics = new InvestmentGroup { Name="Cosmetics",Description="Cosmetics"};
            var networking = new InvestmentGroup { Name="Networking",Description="Networking"};
            var clothing = new InvestmentGroup { Name="Clothing",Description="Clothing"};
            var batteries = new InvestmentGroup { Name="Batteries",Description="Batteries"};
            var onlineGaming = new InvestmentGroup { Name="Online Gaming",Description="Online Gaming"};
            var cinema = new InvestmentGroup { Name="Cinema",Description="Cinema"};
            var outdoorAdvertising = new InvestmentGroup { Name="Outdoor Advertising",Description="Outdoor Advertising"};
            var cloud = new InvestmentGroup { Name="Cloud",Description="Cloud"};
            var ITServices = new InvestmentGroup {Name = "IT services", Description = "IT Services"};
            var consumables = new InvestmentGroup { Name="Consumables",Description="Consumables"};
            var commodoties = new InvestmentGroup { Name="Commodoties",Description="Commodoties"};
            var productMonitoring = new InvestmentGroup { Name="Product Monitoring",Description="Product Monitoring"};
            var technologyUtility = new InvestmentGroup { Name="Technology Utility",Description="Technology Utility"};
            var nutrition = new InvestmentGroup { Name="Nutrition",Description="Nutrition"};
            var mobile = new InvestmentGroup { Name="Mobile",Description="Mobile"};
            var health = new InvestmentGroup { Name="Health",Description="Health"};
            var idendityCredentialManagement = new InvestmentGroup { Name="Idendity & Credential Management",Description="Idendity & Credential Management"};
            var advertisingPromotionLiveEntertainment = new InvestmentGroup { Name="Advertising_Promotion_Live Entertainment",Description="Advertising_Promotion_Live Entertainment"};
            var relationshipsDating = new InvestmentGroup { Name="Relationships/dating",Description="Relationships/dating"};
            var toys = new InvestmentGroup { Name="Toys",Description="Toys"};
            var ledTechnology = new InvestmentGroup { Name="LED Technology",Description="LED Technology"};
            var webIdentity = new InvestmentGroup { Name="Web/Identity",Description="Web/Identity"};
            var stevia = new InvestmentGroup { Name="Stevia",Description="Stevia"};
            var containerTechnology = new InvestmentGroup { Name="Container Technology",Description="Container Technology"};
            var dataAnalyticsSoftwreDeveloper = new InvestmentGroup { Name="Data analytics softwre developer",Description="Data analytics softwre developer"};
            var mobileLocationBasedAdvertising = new InvestmentGroup { Name="Mobile location based Advertising",Description="Mobile location based Advertising"};
            var sportsTechnologyHealth = new InvestmentGroup { Name="Sports/Technology/Health",Description="Sports/Technology/Health"};
            var simulation = new InvestmentGroup { Name="Simulation",Description="Simulation"};
            var musicEntertainment = new InvestmentGroup { Name="Music/Entertainment",Description="Music/Entertainment"};
            var furnitureBedsLowCost = new InvestmentGroup { Name="Furniture/Beds/LowCost",Description="Furniture/Beds/LowCost"};
            var education = new InvestmentGroup { Name="Education",Description="Education"};
            var softwareVentureCapitalists = new InvestmentGroup { Name="Software/Venture Capitalists",Description="Software/Venture Capitalists"};
            var socialMedia = new InvestmentGroup { Name="Social Media",Description="Social Media"};
            var sportsClothing = new InvestmentGroup { Name="Sports/Clothing",Description="Sports/Clothing"};
            var security = new InvestmentGroup { Name="Physical Security",Description="Physical Security"};
            var itServices = new InvestmentGroup { Name = "IT Services", Description = "IT Services" };

            var groups = new List<InvestmentGroup>
            {
                chips,
                internetUtiltiy,
                aiAdvertising,
                semiconductor,
                webAdvertising,
                cosmetics,
                networking,
                clothing,
                batteries,
                onlineGaming,
                cinema,
                outdoorAdvertising,
                cloud,
                cosmetics,
                consumables,
                commodoties,
                productMonitoring,
                cosmetics,
                technologyUtility,
                clothing,
                nutrition,
                mobile,
                health,
                cosmetics,
                idendityCredentialManagement,
                advertisingPromotionLiveEntertainment,
                relationshipsDating,
                toys,
                mobile,
                ledTechnology,
                webIdentity,
                stevia,
                containerTechnology,
                cosmetics,
                dataAnalyticsSoftwreDeveloper,
                mobileLocationBasedAdvertising,
                sportsTechnologyHealth,
                simulation,
                property,
                musicEntertainment,
                furnitureBedsLowCost,
                education,
                softwareVentureCapitalists,
                socialMedia,
                sportsClothing,
                cosmetics,
                security,
                onlineGaming,
                property,
            };

           // Setup a default set of investments

            var albertInvestment = new Investment
            {
                Name = "Albert Technologies",
                Description ="It offers a auotonomous artificial intelligence marketing plaftorm",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var liveNationInvestment = new Investment
            {
                Name = "Live Nation Entertainment Inc",
                Description ="Live Nation Entertainment, Inc. is a live entertainment company. The Company's businesses consist of the promotion of live events, including ticketing, sponsorship and advertising. Its segments include Concerts, Sponsorship & Advertising, Ticketing and Artist Nation. The Concerts segment is engaged in promotion of live music events in its owned or operated venues and in rented third-party venues; operation and management of music venues; production of music festivals, and creation of associated content. The Ticketing segment is an agency business that sells tickets for events on behalf of its clients. The Artist Nation segment provides management services to music artists in exchange for a commission on the earnings of artists. The Sponsorship & Advertising segment employs sales force that creates and maintains relationships with sponsors to allow businesses to reach customers through its concert, venue, artist relationship and ticketing assets, including advertising on its Websites.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var attraqtInvestment = new Investment
            {
                Name = "Attraqt Group Ltd",
                Description ="ATTRAQT Group PLC (ATTRAQT) provides visual merchandising, site search and product recommendation technology. The principal activity of the Company is the development and provision of e-commerce site search, merchandising and product recommendation technology. The Company's Freestyle Merchandising platform provides a range of merchandising disciplines within a single platform. The Company's platform acts as a plugin for a retailer's e-commerce site and provides tools to enable retailers to merchandise. The Company's Freestyle Merchandising enables retailers to control how the products are merchandised through the e-commerce sites, including site search and navigation, product recommendations, category pages, product detail pages, check-out basket, e-mail, order tracking and in-store devices. Over 100 retailers use the ATTRAQT Platform, including various multi-national retailers. The Company's subsidiaries include ATTRAQT Limited and ATTRAQT Inc.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var clearChannelInvestment = new Investment
            {
                Name = "Clear Channel Outdoor Holdings Inc",
                Description ="Clear Channel Outdoor Holdings, Inc. is an outdoor advertising company. The Company provides clients with advertising opportunities through billboards, street furniture displays, transit displays and other out-of-home advertising displays, such as wallscapes and spectaculars. Its segments include Americas outdoor advertising (Americas) and International outdoor advertising (International). The Americas segment consists of operations primarily in the United States, Canada and Latin America. Its Americas assets consist of printed and digital billboards, street furniture and transit displays, airport displays and wallscapes and other spectaculars, which the Company owns or operates under lease management agreements. International segment primarily includes operations in Europe and Asia. The International assets consist of street furniture and transit displays, billboards, mall displays, Smartbike programs, and other spectaculars, which the Company owns or operates under lease agreements.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var amkorInvestment = new Investment
            {
                Name = "Amkor Technology Inc",
                Description ="Amkor Technology, Inc. is a provider of outsourced semiconductor packaging and test services. The Company's packaging and test services are designed to meet application and chip specific requirements, including the type of interconnect technology; size, thickness and electrical, and mechanical and thermal performance. It provides packaging and test services, including semiconductor wafer bump, wafer probe, wafer backgrind, package design, packaging, system-level, and final test and drop shipment services. The Company provides its services to integrated device manufacturers (IDMs), fabless semiconductor companies and contract foundries. IDMs design, manufacture, package and test semiconductors in their own facilities. The Company offers a range of advanced and mainstream packaging and test services. The Company's mainstream packages include leadframe packages, substrate-based wirebond packages and micro-electro-mechanical systems packages.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var ceresInvestment = new Investment
            {
                Name = "Ceres Power Holdings PLC",
                Description ="Ceres Power Holdings PLC is a United Kingdom-based company, which is a fuel cell technology and engineering company. The Company is engaged in the development and commercialization of its fuel cell technology. The SteelCell, operating at a temperature between 500 and 600 degree Celsius, is a perforated sheet of steel with a special ceramic layer that converts fuel directly into electrical power. Its advanced SteelCell technology uses the existing infrastructure of mains natural gas and is manufactured using commodity materials, such as steel. The Company's SteelCell technology is focused on the automotive sector. The Company's subsidiaries include Ceres Power Ltd, Ceres Intellectual Property Company Ltd and Ceres Power Intermediate Holdings Ltd. Ceres Power Ltd is engaged in the development and commercialization of the Company's fuel cell technology.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var underArmourInvestment = new Investment
            {
                Name = "Under Armour Inc",
                Description ="Under Armour, Inc. is engaged in the development, marketing and distribution of branded performance apparel, footwear and accessories for men, women and youth. The Company's segments include North America, consisting of the United States and Canada; Europe, the Middle East and Africa (EMEA); Asia-Pacific; Latin America, and Connected Fitness. Its products are sold across the world and worn by athletes at all levels, from youth to professional, on playing fields around the globe, as well as by consumers with active lifestyles. The Company sells its branded apparel, footwear and accessories in North America through its wholesale and direct to consumer channels. As of December 31, 2016, the Company had approximately 151 factory house stores in North America primarily located in outlet centers throughout the United States. In addition, the Company distributes its products in North America through third-party logistics providers with primary locations in Canada, New Jersey and Florida",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var gildanInvestment = new Investment
            {
                Name = "Gildan Activewear Inc(US)",
                Description ="Gildan Activewear Inc. is a manufacturer and marketer of branded basic family apparel, including T-shirts, fleece, sport shirts, underwear, socks, hosiery and shapewear. The Company operates through two segments: Printwear and Branded Apparel. The Printwear segment designs, manufactures, sources, markets, and distributes undecorated activewear products. The Branded Apparel segment designs, manufactures, sources, markets, and distributes branded family apparel, which includes athletic, casual and dress socks, underwear, activewear, sheer hosiery, legwear, and shapewear products, which are sold to retailers in the United States and Canada. The Company sells its products under various brands, including the Gildan, Gold Toe, Anvil, Comfort Colors, American Apparel, Alstyle, Secret, Silks, Kushyfoot, Secret Silky, Therapy Plus, Peds, and MediPeds brands. The Company distributes its products in printwear markets in the United States, Canada, Mexico, Europe, Asia-Pacific and Latin America.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var avonInvestment = new Investment
            {
                Name = "Avon Products Inc",
                Description ="Avon Products, Inc. is a manufacturer and marketer of beauty and related products. The Company's segments include Europe, Middle East & Africa; South Latin America; North Latin America, and Asia Pacific. Its product categories are Beauty, and Fashion and Home. Beauty consists of skincare (which includes personal care), fragrance and color (cosmetics). Fashion and Home consists of fashion jewelry, watches, apparel, footwear, accessories, gift and decorative products, housewares, entertainment and leisure products, children's products and nutritional products. The Company's products include Anew Ultimate Supreme Advanced Performance Creme, Anew Vitale Visible Perfection Blurring Treatment, Big & Multiplied Volume Mascara, Avon True Perfectly Matte Lipstick, Avon Life for Him and for Her Fragrances, Far Away Infinity Fragrance and Avon Nutra Effects body collection with Active Seed Complex. The Company primarily sells its products to the consumer through the direct-selling channel.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var cotyInvestment =  new Investment
            {
                Name = "Coty Inc",
                Description ="Coty Inc. is a beauty company. The Company operates through four segments: Fragrances, Color Cosmetics, Skin & Body Care and Brazil Acquisition. Its fragrance products include a range of men's and women's products. Its fragrance brands include Calvin Klein, Marc Jacobs, Davidoff, Chloe, Balenciaga, Beyonce, Bottega Veneta, Miu Miu and Roberto Cavalli. Its color cosmetics products include lip, eye, nail and facial color products. The brands in its Color Cosmetics segment include Bourjois, Rimmel, Sally Hansen and OPI. Its skin & body care products include shower gels, deodorants, skin care and sun treatment products. Its skin & body care brands are adidas, Lancaster, philosophy and Playboy. The Company, through Hypermarcas S.A., engages in personal care and beauty business. The Brazil Acquisition segment includes product groupings, such as skin care, nail care, deodorants and hair care products. It operates in the Americas; Europe, the Middle East and Africa (EMEA), and Asia Pacific.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var creightonInvestment = new Investment
            {
                Name = "Creighton's PLC",
                Description ="Creightons plc is engaged in the development, marketing and manufacture of toiletries and fragrances. The Company operates through three business streams: private label business, contract manufacturing business and branded business. Its private label business focuses on private label products for high street retailers and supermarket chains. Its contract manufacturing business develops and manufactures products on behalf of third party brand owners. Its branded business develops, markets, sells and distributes products it has developed and owns the rights to. Its product portfolio includes bath and shower care, haircare, body care, baby and maternity, and fragrances, among others. Its services include market analysis, creative concept generation, product development, brand development, manufacturing and logistics. Its brands include Frizz No More, Volume Pro, Argan Body, Argan Smooth, Keratin Pro, Perfect Hair, Bronze Ambition, Sunshine Blonde, Beautiful Brunette and Just Hair.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var esteeInvestment = new Investment
            {
                Name = "Estee lauder Cos Inc",
                Description ="The Estee Lauder Companies Inc. manufactures and markets skin care, makeup, fragrance and hair care products. The Company offers products, including skin care, makeup, fragrance, hair care and other. The Company operates in beauty products segment. The Company's products are sold in over 150 countries and territories under brand names, including Estee Lauder, Aramis, Clinique, Prescriptives, Lab Series, Origins, Tommy Hilfiger, MAC, Kiton, La Mer, Bobbi Brown, Donna Karan New York, DKNY, Aveda, Jo Malone London, Bumble and bumble, Michael Kors, Darphin, Tom Ford, Smashbox, Ermenegildo Zegna, AERIN, Tory Burch, RODIN olio lusso, Le Labo, Editions de Parfums Frederic Malle, GLAMGLOW, By Kilian, BECCA and Too Faced. Its skin care products include moisturizers, serums, cleansers, toners, body care, exfoliators, acne and oil correctors, facial masks, cleansing devices and sun care products.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var interParfumsInvestment = new Investment
            {
                Name = "Inter parfums Inc",
                Description ="Inter Parfums, Inc. operates in the fragrance business. The Company manufactures, markets and distributes an array of fragrance and fragrance related products. It operates through two segments: European based operations and United States based operations. The European Operations segment produces and distributes its fragrance products under license agreements with brand owners. It has a portfolio of prestige brands, which include Balmain, Boucheron, Coach, Jimmy Choo, Karl Lagerfeld, Lanvin, Paul Smith, S.T. Dupont, Repetto, Rochas, and Van Cleef & Arpels. Its prestige brand fragrance products are also marketed through its United States operations. These fragrance products are sold under various names, which include Abercrombie & Fitch, Agent Provocateur, Anna Sui, bebe, Dunhill, French Connection, Oscar de la Rent and Shanghai Tang brands. The Company sells its products to department stores, perfumeries, specialty stores, and domestic and international wholesalers and distributors.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var revelonInvestment = new Investment
            {
                Name = "Revlon Inc",
                Description ="Revlon, Inc. manufactures, markets and sells around the world a range of beauty and personal care products, including color cosmetics, hair color, hair care and hair treatments, as well as beauty tools, men's grooming products, anti-perspirant deodorants, fragrances, skincare and other beauty care products. The Company operates through four segments: Consumer, which includes cosmetics, hair color and hair care, beauty tools, anti-perspirant deodorants, fragrances and skincare products; Professional, which includes a line of products sold to hair and nail salons, and professional salon distributors, including hair color, shampoos, conditioners, styling products, nail polishes and nail enhancements; Elizabeth Arden, which include Elizabeth Arden, which produces skin care, color cosmetics and fragrances under the Elizabeth Arden brand and Other, which includes the distribution of prestige, designer and celebrity fragrances, cosmetics and skincare products.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var matchInvestment = new Investment
            {
                Name = "Match Group Inc",
                Description ="Match Group, Inc., incorporated on February 13, 2009, is a provider of dating products. The Company operates in the Dating segment. The Dating segment consists of all of its dating businesses across the globe. As of March 31, 2017, the Company operated a portfolio of over 45 brands, including Match, Tinder, PlentyOfFish, Meetic, OkCupid, Pairs, Twoo, OurTime, BlackPeopleMeet and LoveScout24, each designed to manage its users' likelihood of finding a romantic connection. As of March 31, 2017, the Company offered its dating products in 42 languages across more than 190 countries.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var tarenaInvestment = new Investment
            {
                Name = "Tarena International Inc",
                Description ="Tarena International, Inc. (Tarena International) is a holding company. The Company, through its subsidiaries, provides professional education services, including professional information technology (IT) training courses and non-IT training courses across the People's Republic of China (PRC). It operates through training segment. It offers courses in over 10 IT subjects and approximately three non-IT subjects, and over two kid education programs. It offers an education platform that combines live distance instruction, classroom-based tutoring and online learning modules. It complements the live instruction and tutoring with its learning management system, Tarena Teaching System (TTS). TTS has over five core functions, featuring course content, self-assessment exams, student and teaching staff interaction tools, student management tools and an online student community. In addition, the Company offers Tongcheng and Tongmei featuring IT training courses and non-IT training courses.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var amakaInvestment = new Investment
            {
                Name = "Akamai Technologies Inc",
                Description ="Akamai Technologies, Inc. is engaged in providing cloud services for delivering, optimizing and securing content and business applications over the Internet. The Company is involved in offering content delivery network (CDN) services. Its services include the delivery of content, applications and software over the Internet, as well as mobile and security solutions. Its solutions include Performance and Security Solutions, Media Delivery Solutions, and Service and Support Solutions. Its Performance and Security Solutions include Web and Mobile Performance Solutions, Cloud Security Solutions, Enterprise Solutions and Network Operator Solutions. The Media Delivery Solutions offerings include Adaptive Delivery solutions, Download Delivery offerings, Infinite Media Acceleration solutions Media Services and Media Analytics. It offers a range of professional services and solutions designed to assist its customers with integrating, configuring, optimizing and managing its core offerings.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var goldInvestment = new Investment
            {
                Name = "ETFS Physical Gold",
                Description = "ETFS Physical Gold (1672) is designed to offer security holders a simple and cost-efficient way to access the gold market by providing a return equivalent to the movements in the gold spot price less the applicable management fee. 1672 is backed by physical allocated gold held by HSBC Bank plc (the custodian). Only metal that conforms with the London Bullion Market Association's (LBMA) rules for Good Delivery can be accepted by the custodian. Each physical bar is segregated, individually identified and allocated.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            
            var garminInvestment = new Investment
            {
                Name = "Garmin Ltd",
                Description ="Garmin Ltd. (Garmin) and subsidiaries offer global positioning system (GPS) navigation and wireless devices and applications. The Company operates through five segments. It offers a range of auto navigation products, as well as a range of products and applications designed for the mobile GPS market. It offers products to consumers around the world, including Outdoor Handhelds, Wearable Devices, Golf Devices, and Dog Tracking and Training/Pet Obedience Devices. It offers a range of products designed for use in fitness and activity tracking. Garmin offers a range of products designed for use in fitness and activity tracking. Its aviation business segment is a provider of solutions to aircraft manufacturers, existing aircraft owners and operators, as well as military and government customers and serves a range of aircraft, including transport aircraft, business aviation, general aviation, experimental/light sport, helicopters, optionally piloted vehicles and unmanned aerial vehicles.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var nanocoInvestment = new Investment
            {
                Name = "Nanoco Group PLC",
                Description ="Nanoco Group PLC is engaged in research, development and manufacturing of heavy-metal free quantum dots and semiconductor nanoparticles for use in display, lighting, solar energy and bio-imaging. The Company's products include Cadmium Free Quantum Dots (CFQD), CFQD quantum dot films, and copper indium gallium di-selenide (CIGS)/copper indium di-selenide/sulfide (CIS) nanoparticles. The Company's CFQD Quantum Dot Films features include ability to vary blue/red ratio per film; managing heat; customizable size and shape available, and designed to work in conjunction with light emitting diode (LED) from a range of 405 nanometers to 455 nanometers as required. The Company's CFQD quantum dots is a platform technology, which has various applications, including flat screen displays, LED lighting and bio-imaging. The Company's CFQD technology operates in display market, which includes televisions, monitors, notebooks, tablets and smartphones.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var glanbiaInvestment = new Investment
            {
                Name = "Glanbia PLC",
                Description = "Glanbia plc is an Irish global nutrition group with operations in 32 countries. It has leading market positions in sports nutrition, cheese, dairy ingredients, speciality non-dairy ingredients and vitamin and mineral premixes. Glanbia products are sold or distributed in over 130 countries.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var SteinhoffInvestment = new Investment
            {
                Name = "Steinhoff International Holdings NV(Ger)",
                Description = "Steinhoff International Holdings NV is a Germany-based company that is active in the retail of household goods, apparel, as well as in the automotive industry. The household goods business area includes the retail of furniture, building materials and consumer electronics through the Company's subsidiaries Lipo Einrichtungsmaerkte, Poco and Conforama. In the apparel business area the Company operates, among others, through Pepco and is engaged in retailing of women's, men's and children's wear, shoes, and accessories. The Automotive business area includes car rental activities through its subsidiary Hertz, as well as logistics services, warehousing and distribution, agricultural services, supply chain consulting, mining services and passenger transport through its subsidiary Unitrans. The Company operates as a holding company and is present in Europe, Asia, Africa and Australia.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var twitterInvestment = new Investment
            {
                Name = "Twitter Inc (All Sessions)",
                Description = "Twitter, Inc. offers products and services for users, advertisers, developers and data partners. The Company's products and services include Twitter, Periscope, Promoted Tweets, Promoted Accounts and Promoted Trends. Its Twitter is a platform for public self-expression and conversation in real time. Periscope broadcasts can also be viewed through Twitter and on desktop or mobile Web browser. Its Promoted Products enable its advertisers to promote their brands, products and services, amplify their visibility and reach, and extend the conversation around their advertising campaigns. Promoted Accounts appear in the same format and place as accounts suggested by its Who to Follow recommendation engine, or in some cases, in Tweets in a user's timeline. Promoted Trends appear at the top of the list of trending topics for an entire day in a particular country or on a global basis. Its MoPub is a mobile-focused advertising exchange. Twitter Audience Platform is an advertising offering.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var rosslynInvestment = new Investment
            {
                Name = "Rosslyn Data Technologies",
                Description = "Rosslyn Data Technologies plc is a United Kingdom-based company, which is engaged in the development and provision of data analytics software. The Company also offers management services. The Company offers RAPid cloud analytics platform, which is designed for decision-makers. RAPid extracts, combines and synchronizes data from number of sources. Its RAPid cloud analytics platform features a suite of self-service tools business users need to automatically extract, integrate, transform and enrich data. The Company offers a range of platforms, such as technology infrastructure, data factory, application center and security. The Company offers various solutions, including big data solutions; finance solutions; human resource solutions; marketing solutions; procurement solutions; sales solutions; systems, applications, products (SAP) solutions, and Microsoft solutions.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            var scienceInSportInvestment = new Investment
            {
                Name = "Science In Sport Ltd",
                Description = "Science in Sport plc is engaged in developing, manufacturing and marketing sports nutrition products for professional athletes and sports enthusiasts. The Company's product lines include SiS GO isotonic powders and gels, which are digestible carbohydrates for use during exercise; SiS hydration products, which include SiS GO Hydro tablets and SiS GO Electrolyte powders; SiS GO Bars, which include cereal-based food bars; SiS REGO range, which includes drinks and protein bars for recovery after training, and SiS Protein, which is a whey protein range for lean muscle development. The Company offers products in sport categories, including cycling, running, gym, team sports, triathlon and rowing. The Company's products include SiS GO Energy, SiS REGO Rapid Recovery, SiS WHEY20, SiS Whey Protein, SiS GO Isotonic Energy Gel, SiS Elite Team SKY and GO Energy Bar. The Company's subsidiaries include SiS (Science in Sport) Limited, SiS APAC Pty Limited and Science in Sport Inc.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };
            

            var pureCircleInvestment = new Investment
            {
                Name = "Pure Circle Limited",
                Description = "PureCircle Limited is a producer of stevia ingredients for the global food and beverage industry. The Company focuses on encouraging healthier diets around the world through the supply of natural ingredients to the global food and beverage industry. The Company has over 40 stevia-related patents. The Company's Zeta Family ingredients consists of the sugar, such as steviol glycosides, including Reb M and Reb D, and allow for the deepest calorie reductions by food and beverage companies. The Company is engaged in production, marketing and distribution of natural sweeteners and flavors. The Company's geographical segments include Asia, Europe and Americas. The Company is also engaged in the production and marketing of stevia leaf extract. The Company involves in plant breeding, which includes Stevia varieties with sweet glycoside content; harvesting, which provides training to farmers; extraction; purification; application, and finished product. It has offices throughout the world.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };





            var cloudBuyInvestment = new Investment
            {
                Name = "CloudBuy PLC",
                Description = "cloudBuy plc is a provider of an integrated software platform for e-procurement and e-commerce for the trading of goods and services between purchasers, such as public sector bodies and their suppliers, along with the analysis and coding of spend and product data. The Company's operating segments include Company Formation Services, Web and ecommerce services and Coding International Limited. It also provides services to new businesses, including incorporation, company secretary services and filing annual returns, using its software platform. Its solutions include e-commerce Marketplaces, e-commerce Websites, Purchasing Portals, SpendInsight and Company formations. SpendInsight service provides regular analysis of any company's historical spend data. It offers a range of Website packages from templated solutions to Intranets and global business-to-business (B2B) e-commerce sites. The cloudBuy platform enables rapid extension of its solutions and development of new applications.",
                DesirabilityStatement = "Cloud",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            
            var elektronTechnologyInvestment = new Investment
            {
                Name = "Elektron Technology",
                Description = "Elektron Technology plc is a holding company. The Company is engaged in designing, manufacturing and marketing products that connect, monitor and control. It operates in two segments: Connectivity, and Instrumentation, Monitoring and Control (IMC). Connectivity comprises two complementary product families: Bulgin and Arcolectric. The Company's products are helping its customers to quantify real-world environments, process this data and act on the results. Its products include sealed connectors, Switches, indicators, battery, fuseholders, ophthalmic instruments, nanopositioning and sensing equipment, and vehicle power management systems. The Company's Checkit offers a wireless solution providing work management software and automated monitoring. Its subsidiaries include Elektron Technology Corporation, Elektron Technology PTE Ltd, Elektron Technology UK Ltd and Elektron Technology (Shanghai) Trading Limited.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var gluMobileInvestment = new Investment
            {
                Name = "Glu Mobile Inc",
                Description = "Glu Mobile Inc. develops, publishes and markets a portfolio of mobile games. The Company develops and publishes a portfolio of mobile games designed to appeal to a cross section of the users of smartphones and tablet devices. Its portfolio of mobile games is spread across various genres, including Fashion and Celebrity, Food, Sports and Action, Social Networking and Home. Its portfolio of games include Contract Killer, Cooking Dash, Covet Fashion, Deer Hunter, Design Home, QuizUp, Racing Rivals and Tap Sports Baseball, as well as games based on third party licensed brands, including Gordon Ramsay DASH, Kendall & Kylie, and Kim Kardashian: Hollywood. The Company works directly with other application developers to include advertising for their applications in its games, and the developers pay them based on either the number of impressions in its games or the number of users downloading the developer's application.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var gymGroupInvestment = new Investment
            {
                Name = "Gym Group PLC/The",
                Description = "The Gym Group plc is a United Kingdom-based holding company. The Company provides health and fitness facilities. The Company operates approximately 90 gyms across the United Kingdom that are open around the clock. The Company offers gym memberships. Its subsidiaries include The Gym Group Midco1 Limited, The Gym Group Midco2 Limited, The Gym Group Operations Limited and The Gym Limited.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var intercededGroupInvestment = new Investment
            {
                Name = "Interceded Group PLC",
                Description = "Intercede Group plc is a United Kingdom-based software and service company. The Company is engaged in developing and supplying of identity and credential management software. The Company provides MyID software, which is an identity and credential management system that enables organizations to create and assign trusted digital identities to employees, citizens and machines. Its MyID software protects the networks, facilities and intellectual property of governments, agencies and other enterprise customers. In addition, it provides MyTAM, which is a cloud-based service that provides Android application developers and service providers to deploy trusted applications to the trusted execution environment (TEE) on mobile devices. It offers its solutions to various sectors, including aerospace and defense, finance and telecommunications; governments and federal agencies, and mobile developers. The Company operates in the United States and the United Kingdom.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var mattelInvestment = new Investment
            {
                Name = "Mattel Group Inc",
                Description = "Mattel, Inc. manufactures and markets a range of toy products around the world. The Company's segments are North America; International, and American Girl. Its portfolio of brands and products are grouped into approximately four major brand categories, including Mattel Girls & Boys Brands, Fisher-Price Brands, American Girl Brands and Construction and Arts & Crafts Brands. The Mattel Girls & Boys Brands category includes Barbie fashion dolls, Monster High, Disney Classics, Ever After High, Little Mommy, and Polly Pocket, Hot Wheels and Matchbox vehicles and play sets, and CARS, Disney Planes, BOOMco, Toy Story, Max Steel, WWE Wrestling and DC Comics. The Fisher-Price Brands category includes Fisher-Price, Little People, BabyGear, Laugh & Learn, Imaginext, Thomas & Friends, Blaze and The Monster Machines, Shimmer and Shine, Mickey Mouse Clubhouse, Minnie Mouse, Octonauts, and Power Wheels. The Construction and Arts & Crafts Brands category includes MEGA BLOKS, RoseArt and Board Dudes.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var mobileIronInvestment = new Investment
            {
                Name = "MobileIron Inc",
                Description = "MobileIron, Inc. (MobileIron) provides a mobile information technology (IT) platform for enterprises to manage and secure mobile applications, content and devices. The Company's solution provides enterprise security. The MobileIron Platform combines security and enterprise mobility management (EMM) tools, including mobile device management (MDM), mobile application management (MAM), and mobile content management (MCM) capabilities. The Company offers EMM tools, including EMM platform, cloud security with MobileIron Access, Windows security with MobileIron Bridge and applications. MobileIron offers its customers the flexibility to deploy its solution as a cloud service or as on-premises software. Its applications include Apps@Work, Docs@Work, Web@Work, Help@Work, MobileIron Tunnel, MobileIron Rooms, MobileIron AppConnect and AppConnect Ecosystem. The Company serves a range of industries, such as financial services, government, healthcare and legal.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

           

            var oktaInvestment = new Investment
            {
                Name = "Okta Inc",
                Description = "Okta, Inc., is an independent provider of identity for the enterprise. The Company's Okta Identity Cloud platform provides identity management solutions that enable customers to secure their users and connect them to technology and applications. It also connects enterprises to their customers, employees, contractors, and partners. It allows users to access a range of cloud applications, Websites, mobile applications and service from various devices. Its platform is used by information technology (IT) organizations to secure their enterprise and by developers to build customer-facing Websites and applications. Okta Identity Cloud consists of a suite of products to manage and secure identities. It offers a range of products, such as Adaptive Multi-Factor Authentication, Universal Directory, Lifecycle Management products, Single Sign-On, application program interface (API) Access Management and Mobility Management.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var rm2InternationalInvestment = new Investment
            {
                Name = "RM2 International SA",
                Description = "RM2 International S.A. is a pallet development, manufacture, supply and management company. The Company is principally engaged in developing and selling shipping pallets and providing related logistical services. The Company's product for moving goods, BLOCKPal, has impermeability to water and contamination, fire retardancy, and resistance to damage and weight. The Company also offers systems for tracking asset movements and for optimizing the utilization and logistics of those assets. The Company's ERICA system provides real time intelligence to monitor and manage the movement of any transit equipment. The Company also offers a pallet rental program. The Company also offers supply chain auditing and consulting services, including measuring a supply chain's efficiency, determining the viability of a closed loop system, weighing the advantages of an open architecture and monetizing inbound pallet movements.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var sitoMobileInvestment = new Investment
            {
                Name = "SITO Mobile Limited",
                Description = "SITO Mobile, Ltd. operates a mobile location-based advertising platform serving businesses, advertisers and brands. The Company's offerings include SITO Location-Based Advertising and SITO Mobile Messaging. SITO Location-Based Advertising delivers display advertisements and videos on behalf of advertisers, including various features, such as Geo-fencing, Verified walk-in, Behavioral Targeting, and Analytics and Optimization. Geo-fencing targets customers within a certain radius of location and uses technology to push coupons, advertisements and promotions to mobile applications. Verified Walk-in tracks foot-traffic to locations and which advertisements drive action. Behavioral Targeting tracks past behaviors over 30 to 90 day increments allowing for real-time campaign management. Analytics and Optimization is a culling and building measurement system. SITO Mobile Messaging is a platform for building and controlling programs, including messaging and customer incentive programs.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var simiGonInvestment = new Investment
            {
                Name = "SimiGon Ltd",
                Description = "SimiGon Ltd is engaged in developing learning, training and simulation technologies and applications for use in professional communities. The Company has developed SIMbox, a personal computer (PC)-based software platform to create, modify, manage and deploy any simulation-based content across a multitude of domains, such as training, research development, operations analysis and entertainment. SIMbox is a three dimensional (3D) simulation engine including software modules that enable users to create new products and content. The Company also provides KnowBook is a training solution for aircrew and organizations. KnowBook includes all types of simulation, knowledge management, mission rehearsal, after action review (AAR), and time management. The Knowbook family consists of AirBook, GroundBook, and MarineBook. In addition, the Company offers AirTrack, which provides passenger inflight entertainment solutions, and D-Brief PC, an offline/real-time debriefing application.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var simonPropertyGroupInvestment = new Investment
            {
                Name = "Simon Property Group Inc",
                Description = "Simon Property Group, Inc. is a self-administered and self-managed real estate investment trust (REIT). The Company owns, develops and manages retail real estate properties, which consist primarily of malls, Premium Outlets and The Mills. Simon Property Group, L.P. (Operating Partnership), is the Company's partnership subsidiary that owns all of its real estate properties and other assets. As of December 31, 2016, the Company owned or held an interest in 206 income-producing properties in the United States, which consisted of 108 malls, 67 Premium Outlets, 14 Mills, four lifestyle centers, and 13 other retail properties in 37 states and Puerto Rico. As of December 31, 2016, it had redevelopment and expansion projects, including the addition of anchors, big box tenants, and restaurants, underway at 27 properties in the United States and it had one outlet and one other retail project under development.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var spotifyInvestment = new Investment
            {
                Name = "Spotify Technology SA",
                Description = "Spotify Technology SA a Luxembourg-based company, which offers digital music-streaming services. The Company enables users to discover new releases, which includes the latest singles and albums; playlists, which includes ready-made playlists put together by music fans and experts, and over millions of songs so that users can play their favorites, discover new tracks and build a personalized collection. Users can either select Spotify Free, which includes only shuffle play or Spotify Premium, which encompasses a range of features, such as shuffle play, advertisement free, unlimited skips, listen offline, play any track and high quality audio. The Company operates through a number of subsidiaries, including Spotify LTD and is present in over 20 countries.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var starcomInvestment = new Investment
            {
                Name = "Starcom Plc",
                Description = "Starcom plc is engaged in the development of wireless solutions for the remote tracking, monitoring and protection of various types of assets and people. The Company, along with its subsidiaries, has four operating segments: sets, accessory, Web and other. The Company has two wholly owned subsidiaries: Starcom G.P.S. Systems Ltd. and Starcom Systems Limited. Starcom G.P.S. Systems Ltd. is an Israeli company that engages in the same field. The Company offers systems, including Helios, Triton, WatchLock, Rainbow, Kylos and the Online Application. Helios is an automatic vehicle location and fleet management systems. Triton R container tracking system provides ongoing monitoring of containers. WatchLock is a security padlock. Kylos is a portable tracking solution for locating merchandise assets, people and pets. Starcom Online, an online application, provides online support.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var ternInvestment = new Investment
            {
                Name = "Tern Plc",
                Description = "Tern Plc invests in, develops and sells private software companies with technology, based in the United Kingdom. The principal activity of the Company is investing in unquoted and quoted companies to achieve capital growth. The Company focuses on businesses in the cloud, Internet of Things (IOT) and mobile sectors. The Company focuses on building companies with technologies and services within the IOT market.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var usuSoftwareInvestment = new Investment
            {
                Name = "USU Software AG",
                Description = "USU Software AG is a Germany-based company that develops and markets end-to-end software solutions. It operates through two sectors: Product Business and Service Business. The Product Business sector includes products and services in the areas of infrastructure management, software license management, service/change management, finance management, process management and knowledge management. The Service Business sector encompasses consulting services for Information Technology (IT) projects and individual application development. The Company has subsidiaries in Germany, Switzerland, the Czech Republic, Austria and the United States, such as unitB technology GmbH and SecurIntegration GmbH. Its customers operate mainly in the field of financial services, telecommunications, the automotive industry, consumer goods, services and trade, as well as the public sector.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var ubisenseInvestment = new Investment
            {
                Name = "Ubisense Group PLC",
                Description = "Ubisense Group Plc is engaged in providing enterprise location intelligence solutions for manufacturing, logistics, transit, communication and utility companies. The Company operates through Geospatial. The RTLS segment takes real-time location data from its own sensing hardware, or from standards-based integration with third party hardware, and transforms this data into spatial event information, delivering asset identification, real-time location and spatial monitoring. The Geospatial segment delivers software solutions that integrate data from any source, including geographic, real-time asset, global positioning system, location, corporate and external cloud-based sources into a live Geospatial common operating picture. The Company offers various products, such as Smart Factory, myWorld, myWorld Damage Assessment, myWorld Inspection & Survey, myWorld Network Insight, Dimension4 and AngleID.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };
            

            var warpaintLondonInvestment = new Investment
            {
                Name = "Warpaint London PLC",
                Description = "Warpaint London PLC is a United Kingdom-based company engaged in color cosmetics business. The Company sells color cosmetics in the United Kingdom and overseas, principally under the W7 brand. The Company operates through two divisions: close-out and own-brand. The own-brand division consists primarily of the Company's flagship brand, W7. The W7 brand contains over 500 items, which are sold into high street retailers and independent beauty shops across the United Kingdom, Europe, Australia and the United States. The W7 brand focuses on the 16-30 age range.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var westminsterGroupInvestment = new Investment
            {
                Name = "Westminster Group PLC",
                Description = "Westminster Group PLC is a security and services company. The Company's principal activity is the design, supply and ongoing support of technology security solutions and the provision of long term managed services, consultancy and training services. It operates through two divisions, which include Managed Services and Technology. Its Managed Services division is focused on long term recurring revenue managed services contracts, such as the management and running of complete security solutions in airports, ports and other such facilities, together with the provision of ferry services, manpower, consultancy and training services. Its Technology division is focused on providing technology led security solutions encompassing a range of surveillance, detection, tracking, screening and interception technologies to governments and organizations across the world. The Company's subsidiaries include Westminster International Limited and Longmoor Security Limited.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var zyngaInvestment = new Investment
            {
                Name = "Zynga Inc",
                Description = "Zynga Inc. is a provider of social game services. The Company develops, markets and operates social games as live services played on mobile platforms, such as iPhone Operating System (iOS) operating system and Android operating system and social networking sites, such as Facebook. The Company has developed a range of social games, including games in its Slots, Words With Friends, Zynga Poker and FarmVille franchises. It operates its games as live services and updates them with new features. It analyzes the data generated by its players' game play and social interactions to guide the creation of new content and features. The Company operates its games as live services that are available anytime and anywhere. The Company invests in game categories, including Social Casino, Casual, Action Strategy and Invest Express. Social Casino includes Zynga Poker and its Slots games, such as Hit It Rich! Slots, Wizard of Oz Slots, Willy Wonka and the Chocolate Factory Slots, and Black Diamond Casino.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var iSharesUkPropertyInvestment = new Investment
            {
                Name = "iShares UK Property UCITS ETF",
                Description = "The Fund seeks to track the performance of an index composed of UK listed real estate companies and Real Estate Investment Trusts (REITS).",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()

            };

            var axaProperty = new Investment
            {
                Name = "AXA Property Trust LTD",
                Description = "AXA Property Trust Limited is a closed-ended investment company. The Company's investment objective is to be managed with the intention of realizing all remaining assets in the Portfolio, in a manner consistent with the principles of prudent investment management and spread of investment risk, with a view to returning capital invested to the shareholders in an orderly manner. The Company invests in commercial properties in Europe, which are held through its subsidiaries. The Company's portfolio includes in geographic locations, such as Germany and Italy. The Company invests in retail, industrial and leisure sectors. AXA Investment Managers UK Limited serves as the investment manager to the Company.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var advancedMicrosDevices = new Investment
            {
                Name = "Advanced Micro Devices",
                Description = "Advanced Micro Devices, Inc. is a global semiconductor company. The Company is engaged in offering x86 microprocessors, as standalone devices or as incorporated into an accelerated processing unit (APU), chipsets, discrete graphics processing units (GPUs) and professional graphics, and server and embedded processors and semi-custom System-on-Chip (SoC) products and technology for game consoles. The Company's segments include the Computing and Graphics segment, and the Enterprise, Embedded and Semi-Custom segment. The Computing and Graphics segment primarily includes desktop and notebook processors and chipsets, discrete GPUs and professional graphics. The Enterprise, Embedded and Semi-Custom segment primarily includes server and embedded processors, semi-custom SoC products, development services, technology for game consoles and licensing portions of its intellectual property portfolio.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var canadaGoose = new Investment
            {
                Name = "Canada Goose Holdings Inc",
                Description = "Canada Goose Holdings Inc. is a Canadian holding company of winter clothing manufacturers. The company was founded in 1957 by Sam Tick, under the name Metro Sportswear Ltd. Canada Goose maintains a wide range of jackets, parkas, vests, hats, gloves, shells and other apparel.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var changyou = new Investment
            {
                Name = "changyou.com Ltd - ADR",
                Description = "Changyou.com Limited is an online game developer and operator. The Company is engaged in the development, operation and licensing of online games for personal computers (PCs) and mobile devices. The Company's segments include Online Game segment, which consists primarily of PC games and mobile games; the Platform Channel segment, which consists primarily of online advertising services offered on the 17173.com Website, Internet value-added services (IVAS) offered on the Dolphin Browser and RaidCall and online card and board games offered by MoboTap, and the Cinema Advertising segment, which consists primarily of the acquisition, from operators of movie theaters, and the sale, to advertisers, of pre-film advertising slots, which are advertisements shown before the screening of a movie in a cinema theater. Its online games include a range of genres, including massively multi-player online role-playing games (MMORPGs), third person shooter games (TPSs) and collectible card games (CCGs).",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var cloudera = new Investment
            {
                Name = "Cloudera Inc",
                Description = "Cloudera is a developer of a platform for data managment, machine learning and advanced analytics",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var wipro = new Investment
            {
                Name = "Wipro Ltd  - ADR",
                Description = "Wipro is a global IT consulting and business service provider",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var tencent = new Investment
            {
                Name = "Tencent Music Entertainment Group",
                Description = "TENCENT MUSIC ENTERTAINMENT GROUP operates online music entertainment platform and music applications in China. The Company's platform comprises online music, online karaoke and music-centric live streaming services, supported by content offerings, technology and data. The Company's main platform includes QQ Music, Kugou Music, Kuwo Music, WeSing, Kugou Live, Kuwo Live and others.",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };

            var adtranInvestment = new Investment
            {
                Name = "ADTRAN",
                Description = "ADTRAN is a provider of networking and communications equipment",
                DesirabilityStatement = "",
                InitialInvestment = 2,
                Symbol = "$",
                Value = 2,
                ValueProposition = "",
                Factors = new List<InvestmentInfluenceFactor_Investment>(),
                Groups = new List<InvestmentGroup_Investment>()
            };
            



            var investments = new List<Investment>()
            {
                albertInvestment,
                adtranInvestment,
                pureCircleInvestment,
                scienceInSportInvestment,
                rosslynInvestment,
                twitterInvestment,
                SteinhoffInvestment,
                glanbiaInvestment,
                nanocoInvestment,
                garminInvestment,
                goldInvestment,
                amakaInvestment,
                tarenaInvestment,
                matchInvestment,
                revelonInvestment,
                interParfumsInvestment,
                creightonInvestment,
                cotyInvestment,
                avonInvestment,
                gildanInvestment,
                underArmourInvestment,
                ceresInvestment,
                amkorInvestment,
                clearChannelInvestment,
                attraqtInvestment,
                liveNationInvestment,
                cloudBuyInvestment,
                elektronTechnologyInvestment,
                gluMobileInvestment,
                gymGroupInvestment,
                intercededGroupInvestment,
                mattelInvestment,
                mobileIronInvestment,
                oktaInvestment,
                rm2InternationalInvestment,
                sitoMobileInvestment,
                simiGonInvestment,
                simonPropertyGroupInvestment,
                spotifyInvestment,
                starcomInvestment,
                ternInvestment,
                usuSoftwareInvestment,
                ubisenseInvestment,
                underArmourInvestment,
                warpaintLondonInvestment,
                westminsterGroupInvestment,
                zyngaInvestment,
                iSharesUkPropertyInvestment,
                axaProperty,
                advancedMicrosDevices,
                canadaGoose,
                changyou,
                cloudera,
                wipro,
                tencent

            };
            investments.ForEach(i =>
            {
                i.Factors = new List<InvestmentInfluenceFactor_Investment>();
                i.Groups = new List<InvestmentGroup_Investment>();
                i.Risks = new List<InvestmentRisk_Investment>();
                i.Regions = new List<Region_Investment>();
            });
            
            db.Groups.AddRange(groups);
            db.Investments.AddRange(investments);
            db.SaveChanges();
            

            // Create a long company description for this investment
            int maxLineLength = 160;
            foreach (var dbInvestment in db.Investments)
            {
                var longCompanyDesc = new InvestmentNote
                {
                    Description = dbInvestment.Description,
                    OwningEntityId = dbInvestment.Id,
                    OwningEntityType = EntityType.Investment,
                    Name = "Long company description",
                };    
                
                db.Notes.Add(longCompanyDesc);
            }
            db.SaveChanges();

            

            void SetupInvestment(Investment investment, InvestmentGroup[] invGroups)
            {
                var list = invGroups.Select(group => new InvestmentGroup_Investment
                {
                    InvestmentGroup = group, 
                    Investment = investment, 
                    InvestmentGroupID = group.Id, 
                    InvestmentID = investment.Id
                }).ToList();

                list.ForEach(i=>investment.Groups.Add(i));
            }
            
            // configure investments
            
            SetupInvestment(pureCircleInvestment, new []{ stevia });
            SetupInvestment(scienceInSportInvestment, new []{ sportsTechnologyHealth });
            SetupInvestment(twitterInvestment, new []{ socialMedia });
            SetupInvestment(SteinhoffInvestment, new []{ furnitureBedsLowCost });
            SetupInvestment(glanbiaInvestment, new []{ nutrition });
            SetupInvestment(nanocoInvestment, new []{ ledTechnology });
            SetupInvestment(garminInvestment, new []{ technologyUtility });
            SetupInvestment(goldInvestment, new []{ commodoties });
            SetupInvestment(amakaInvestment, new []{ internetUtiltiy });
            SetupInvestment(tarenaInvestment, new []{ education });
            SetupInvestment(matchInvestment, new []{ relationshipsDating });
            SetupInvestment(revelonInvestment, new []{ cosmetics });
            SetupInvestment(interParfumsInvestment, new []{ cosmetics });
            SetupInvestment(creightonInvestment, new []{ consumables });
            SetupInvestment(cotyInvestment, new []{ cosmetics });
            SetupInvestment(avonInvestment, new []{ cosmetics });
            SetupInvestment(gildanInvestment, new []{ sportsClothing });
            SetupInvestment(underArmourInvestment, new []{ sportsClothing });
            SetupInvestment(ceresInvestment, new []{ batteries });
            SetupInvestment(amkorInvestment, new []{ semiconductor });
            SetupInvestment(clearChannelInvestment, new []{ outdoorAdvertising });
            SetupInvestment(attraqtInvestment, new []{ webAdvertising });
            SetupInvestment(liveNationInvestment, new []{ advertisingPromotionLiveEntertainment });
            SetupInvestment(cloudBuyInvestment, new []{ cloud });
            SetupInvestment(elektronTechnologyInvestment, new []{ productMonitoring });
            SetupInvestment(gluMobileInvestment, new []{ mobile });
            SetupInvestment(gymGroupInvestment, new []{ health });
            SetupInvestment(intercededGroupInvestment, new []{ idendityCredentialManagement });
            SetupInvestment(mattelInvestment, new []{ toys });
            SetupInvestment(mobileIronInvestment, new []{ mobile });
            SetupInvestment(oktaInvestment, new []{ idendityCredentialManagement });
            SetupInvestment(rm2InternationalInvestment, new []{ containerTechnology });
            SetupInvestment(sitoMobileInvestment, new []{ mobileLocationBasedAdvertising });
            SetupInvestment(simiGonInvestment, new []{ simulation });
            SetupInvestment(simonPropertyGroupInvestment, new []{ property });
            SetupInvestment(spotifyInvestment, new []{ musicEntertainment });
            SetupInvestment(starcomInvestment, new []{ stevia });
            SetupInvestment(ternInvestment, new []{ softwareVentureCapitalists });
            SetupInvestment(usuSoftwareInvestment, new []{ stevia });
            SetupInvestment(ubisenseInvestment, new []{ stevia });
            SetupInvestment(warpaintLondonInvestment, new []{ cosmetics });
            SetupInvestment(westminsterGroupInvestment, new []{ security });
            SetupInvestment(zyngaInvestment, new []{ onlineGaming });
            SetupInvestment(iSharesUkPropertyInvestment, new []{ property });
            SetupInvestment(axaProperty, new[]{ property } );
            SetupInvestment(advancedMicrosDevices, new[] { chips });
            SetupInvestment(canadaGoose, new[] { clothing });
            SetupInvestment(changyou, new[] { onlineGaming });
            SetupInvestment(cloudera, new[] { cloud });
            SetupInvestment(wipro, new[] { itServices });
            SetupInvestment(tencent, new[] { musicEntertainment });
            

            db.SaveChanges();
            
        }
    }

    public class CustomEntitiesInitializer : IDbInitializer
    {
        public void Initialize(InvestmentDbContext db)
        {
            if (db.CustomEntityTypes.Any()) return;

            
            var customEntityTypes = new[]
            {
                new CustomEntityType {Description = "Products sold by this investment", Name = "Products"},
                new CustomEntityType {Description = "Services sold by this investment", Name = "Services"},
                new CustomEntityType {Description = "Part of a business that can be distinctly separated from the company as a whole based on its customers, products, or market places", Name = "Business segments"},
                new CustomEntityType {Description = "Investor Review", Name = "Investment review"},
            };

            db.CustomEntityTypes.AddRange(customEntityTypes);
            
            db.SaveChanges();

        }
    }

    public class AdditionalDbInitializer : IDbInitializer
    {
        public void Initialize(InvestmentDbContext db)
        {
           

            const int max = 10;
            var factors = new List<InvestmentInfluenceFactor> {
                    new InvestmentInfluenceFactor { Name = "Weather", Description = "The climate will affect the investment.", Influence = "Sunny weather helps, rainy weather doesn't"},
                    new InvestmentInfluenceFactor { Name = "Competion", Description = "The competition dictates te supply and demand", Influence = "The more cometition the less buiness you get if the competition or on par to you"},
            };

            var sampleInfluenceFactors = new[]
            {
                    "Transport",
                    "Travel/Tourism",
                    "Utilities",
                    "Telecommunications",
                    "Professional Services/Consulting",
                    "Pharmaceutical/Medical Product",
                    "Oil/Gas",
                    "Mining/Metals",
                    "Manufacturing",
                    "IT (Hardware/Software/Services)",
                    "Investment Banking",
                    "Food and Beverage",
                    "Consumer Goods",
                    "Agriculture"
            };
            foreach (var each in sampleInfluenceFactors)
            {
                var f = new InvestmentInfluenceFactor { Name = each, Description = "description about " + each };
                f.Influence = "influence by/how/why xyx " + each;
                factors.Add(f);
            }
            factors.ForEach(f => f.Investments = new List<InvestmentInfluenceFactor_Investment>());

            // only add if we've not added these groups before
            var neverAddedTheseFactorsbefore = db.Factors.SingleOrDefault(o => o.Name.Equals("Agriculture")) == null;
            if (neverAddedTheseFactorsbefore)
            {
                factors.ForEach(f => db.Factors.Add(f));
            }

            db.SaveChanges();

            if (neverAddedTheseFactorsbefore)
            {
                // make some notes for factors
                var notes = factors.Select(f => new InvestmentNote
                {
                    OwningEntityType = Common.EntityType.InvestmentInfluenceFactor,
                    OwningEntityId = f.Id,
                    Description = "Note contents for factor " + f.Name,
                    Name = "Title" + f.Name
                });
                db.Notes.AddRange(notes);
                db.SaveChanges();
            }

            var groups = new List<InvestmentGroup> {
                new InvestmentGroup{ Name = "Value Investments", Description = "high current p/e with potential to maintain.", Type = "" },
                new InvestmentGroup{ Name = "Growth Investments", Description = "Low p/e with potential to grow", Type = "" },
                new InvestmentGroup{ Name = "Momentum Investments", Description = "Fashionalble trends", Type = "" },
                new InvestmentGroup{ Name = "Hybrid Investments", Description = "Bit of everything", Type = "" },
                new InvestmentGroup{ Name = "Tactical", Description = "carefully considered group", Type = "" },
                new InvestmentGroup{ Name = "Strategic", Description = "Assets with a strategic goal associated with them", Type = "" },
                new InvestmentGroup{ Name = "Shares", Description = "Equity in company shares - fractional part owner", Type = "" },
                new InvestmentGroup{ Name = "Gold", Description = "ommodity which is valuable when markets are volatile", Type = "" },
                new InvestmentGroup{ Name = "Emerging markets", Description = "places like Japan, Turkey, Brazil, Taiwan etc.", Type = "" },
            };

            groups.ForEach(g => {
                g.Children = new List<InvestmentGroup>();
                InvestmentGroup child = new InvestmentGroup { Name = g.Name + "Child", Description = "Child", Type = "", Parent = g };
                g.Children.Add(child);
            });
            groups.ForEach(g => g.Investments = new List<InvestmentGroup_Investment>());

            var neverAddedTheseGroupsBefore = db.Groups.SingleOrDefault(g => g.Name.Equals("Emerging markets")) == null;
            if (neverAddedTheseGroupsBefore)
            {
                groups.ForEach(g => db.Groups.Add(g));

                db.SaveChanges();

                // make some group notes
                List<InvestmentNote> group_notes = new List<InvestmentNote>();
                groups.ForEach(r =>
                {
                    group_notes.Add(new InvestmentNote
                    {
                        Name = "note for risk " + r.Name,
                        Description = "description for " + r.Description,
                        OwningEntityId = r.Id,
                        OwningEntityType = Common.EntityType.InvestmentGroup
                    });
                });

                db.Notes.AddRange(group_notes);
                db.SaveChanges();

            }

            var regions = new List<Region> {
                new Region { Name = "Canada"},
                new Region { Name = "Russia"},
                new Region { Name = "Asia"},
                new Region { Name = "South America"},
                new Region { Name = "North America"},
                new Region { Name = "Global"},
                new Region { Name = "Europe" },
                new Region { Name = "Africa" },
                new Region { Name = "Australia" },
                new Region { Name = "Americas" },
                new Region { Name = "Japan" },
                new Region { Name = "Germany" },
                new Region { Name = "Mexico" },
                new Region { Name = "South Africa" },

            };

            regions.ForEach(r => r.Investments = new List<Region_Investment>());

            var neverAddedTheseRegionsBefore = db.Regions.SingleOrDefault(r => r.Name.Equals("Mexico")) == null;
            if (neverAddedTheseRegionsBefore)
            {
                regions.ForEach(r => db.Regions.Add(r));
                db.SaveChanges();

                // make some region notes
                List<InvestmentNote> region_notes = new List<InvestmentNote>();
                regions.ForEach(r =>
                {
                    region_notes.Add(new InvestmentNote
                    {
                        Name = "note for risk " + r.Name,
                        Description = "description for " + r.Description,
                        OwningEntityId = r.Id,
                        OwningEntityType = EntityType.Region
                    });
                });

                db.Notes.AddRange(region_notes);
                db.SaveChanges();

            }

            

            var risks = new List<InvestmentRisk> {
                new InvestmentRisk { Name = "Director dismissal", Description = "Financial officer fired due to corruption", Type = Common.RiskType.Company },
                new InvestmentRisk { Name = "Competition", Description = "Competition from other companies", Type = Common.RiskType.Company },
                new InvestmentRisk { Name = "Fashion", Description = "Fashion/popularity of the comodity", Type = Common.RiskType.Company },
                new InvestmentRisk { Name = "Earnings report", Description = "Investor perception based on earnings", Type = Common.RiskType.Company },
            };

            risks.ForEach(r => r.Investments = new List<InvestmentRisk_Investment>());
            
            var neverAddedtheseRisksBefore = db.Risks.SingleOrDefault(r => r.Name.Equals("Director dismissal")) == null;
            if (neverAddedtheseRisksBefore)
            {
                risks.ForEach(r => db.Risks.Add(r));
                db.SaveChanges();

                // make some risk notes
                List<InvestmentNote> risk_notes = new List<InvestmentNote>();
                risks.ForEach(r =>
                {
                    risk_notes.Add(new InvestmentNote
                    {
                        Name = "note for risk " + r.Name,
                        Description = "description for " + r.Description,
                        OwningEntityId = r.Id,
                        OwningEntityType = Common.EntityType.InvestmentRisk
                    });
                });

                db.Notes.AddRange(risk_notes);
                db.SaveChanges();
            }

            var neverAddedRandomeInvestmentsbefore = !db.Investments.Any(i => i.Name.StartsWith("investment#"));
            if (neverAddedRandomeInvestmentsbefore)
            {
                var investments = new List<Investment>();
                for (int i = 0; i < max; i++)
                {
                    var investment = new Investment
                    {
                        Description = "Description",
                        Symbol = "£",
                        DesirabilityStatement = "default desirabliity statement#" + i,
                        InitialInvestment = i,
                        Name = "investment#" + i,
                        Value = i,
                        ValueProposition = "value proposition#" + i
                    };

                    investment.Factors = new List<InvestmentInfluenceFactor_Investment>();
                    investment.Groups = new List<InvestmentGroup_Investment>();
                    investment.Regions = new List<Region_Investment>();
                    investment.Risks = new List<InvestmentRisk_Investment>();

                    // now randonly assign some of the risks/factos/groups/regions to this investment
                    var rnd = new Random(DateTime.Now.Millisecond);
                    var alreadyUsed = new HashSet<int>();

                    int GetUnusedRandom(int maxRange)
                    {
                        var random = rnd.Next(1, maxRange);
                        while (alreadyUsed.Contains(random))
                        {
                            random = rnd.Next(1, maxRange);
                        }

                        alreadyUsed.Add(random);
                        return random;
                    }

                    int gmax = GetUnusedRandom(groups.Count);

                    for (int g = 0; g < gmax; g++)
                    {
                        InvestmentGroup_Investment link = new InvestmentGroup_Investment
                        {
                            Investment = investment,
                            InvestmentGroup = groups[g]
                        };
                        investment.Groups.Add(link);
                    }

                    int fmax = GetUnusedRandom( factors.Count);

                    for (int f = 0; f < fmax; f++)
                    {
                        InvestmentInfluenceFactor_Investment link = new InvestmentInfluenceFactor_Investment
                        {
                            Investment = investment,
                            InvestmentInfluenceFactor = factors[f]
                        };
                        investment.Factors.Add(link);
                    }

                    int rmax = GetUnusedRandom(risks.Count);

                    for (int r = 0; r < rmax; r++)
                    {
                        InvestmentRisk_Investment link = new InvestmentRisk_Investment
                        {
                            Investment = investment,
                            InvestmentRisk = risks[r]
                        };
                        investment.Risks.Add(link);
                    }

                    int regionmax = GetUnusedRandom( regions.Count);
                    for (int r = 0; r < regionmax; r++)
                    {
                        Region_Investment link = new Region_Investment
                        {
                            Investment = investment,
                            Region = regions[r]
                        };

                        investment.Regions.Add(link);
                    }

                    investments.Add(investment);
                }

                investments.ForEach(inv => db.Investments.Add(inv));
                db.SaveChanges();


                // make some investment notes

                db.Notes.AddRange(investments.Select(m => new InvestmentNote
                {
                    OwningEntityType = Common.EntityType.Investment,
                    OwningEntityId = m.Id,
                    Name = "note for " + m.Name,
                    Description = "note for " + m.Name
                }));
                db.SaveChanges();
            }

            
        }
    }
}