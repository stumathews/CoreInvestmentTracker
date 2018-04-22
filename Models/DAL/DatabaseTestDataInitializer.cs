using CoreInvestmentTracker.Models.DEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreInvestmentTracker.Models.DAL
{
    /// <summary>
    /// This class is setup in the web.config to create new data in the database at startup.
    /// Its mentioned in the web.config
    /// </summary>
    public class DatabaseTestDataInitializer
    {
        const int MAX = 10;
        /// <summary>
        /// This method(seed) basically creates and adds our entities to our derived DbContext object,
        /// which has a list(DbSet) for each of the entities we want to persist.
        /// We basically create those entities here by hand and add them to that list in the DbContext.
        /// We then ask the DBContext to SaveChanges() which persists what we've setup to be added to those lists(managed by the DbContext)
        /// </summary>
        /// <param name="db"></param>
        public static void Initialize(ApplicationDbContext db)
        {
            db.Database.EnsureCreated();

            if (db.Investments.Any())
            {
                return;   // DB has been seeded
            }

            var inits = new List<IDbInitializer>(new IDbInitializer[]{ new DbInitilizer2(), new Dbinitializer1()});
            inits.ForEach(initializer => initializer.Initialize(db));



        }
    }

    public interface IDbInitializer
    {
        void Initialize(ApplicationDbContext db);
    }

    public class DbInitilizer2 : IDbInitializer
    {
        public void Initialize(ApplicationDbContext db)
        {
            

            var investments = new List<Investment>(new []
            {
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
                {
                    Name = "ETFS Physical Gold",
                    Description ="",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
                {
                    Name = "Yolo Leisure and technology",
                    Description ="YOLO Leisure and Technology Plc, formerly Pentagon Protection Plc, is an investment company. The Company focuses on opportunities in the travel, technology and leisure sectors. The Company's primary objective is that of securing for the shareholders the possible value consistent with achieving, over time, both capital growth and income for shareholders through developing profitability coupled with dividend payments on a sustainable basis.",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
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

                },
                new Investment
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

                },
                new Investment
                {
                    Name = "Glanbia PLC",
                    Description ="",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
                {
                    Name = "Steinhoff International Holdings NV(Ger)",
                    Description ="Steinhoff International Holdings NV is a Germany-based company that is active in the retail of household goods, apparel, as well as in the automotive industry. The household goods business area includes the retail of furniture, building materials and consumer electronics through the Company's subsidiaries Lipo Einrichtungsmaerkte, Poco and Conforama. In the apparel business area the Company operates, among others, through Pepco and is engaged in retailing of women's, men's and children's wear, shoes, and accessories. The Automotive business area includes car rental activities through its subsidiary Hertz, as well as logistics services, warehousing and distribution, agricultural services, supply chain consulting, mining services and passenger transport through its subsidiary Unitrans. The Company operates as a holding company and is present in Europe, Asia, Africa and Australia.",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
                {
                    Name = "Twitter Inc (All Sessions)",
                    Description ="Twitter, Inc. offers products and services for users, advertisers, developers and data partners. The Company's products and services include Twitter, Periscope, Promoted Tweets, Promoted Accounts and Promoted Trends. Its Twitter is a platform for public self-expression and conversation in real time. Periscope broadcasts can also be viewed through Twitter and on desktop or mobile Web browser. Its Promoted Products enable its advertisers to promote their brands, products and services, amplify their visibility and reach, and extend the conversation around their advertising campaigns. Promoted Accounts appear in the same format and place as accounts suggested by its Who to Follow recommendation engine, or in some cases, in Tweets in a user's timeline. Promoted Trends appear at the top of the list of trending topics for an entire day in a particular country or on a global basis. Its MoPub is a mobile-focused advertising exchange. Twitter Audience Platform is an advertising offering.",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
                {
                    Name = "Rosslyn Data Technologies",
                    Description ="Rosslyn Data Technologies plc is a United Kingdom-based company, which is engaged in the development and provision of data analytics software. The Company also offers management services. The Company offers RAPid cloud analytics platform, which is designed for decision-makers. RAPid extracts, combines and synchronizes data from number of sources. Its RAPid cloud analytics platform features a suite of self-service tools business users need to automatically extract, integrate, transform and enrich data. The Company offers a range of platforms, such as technology infrastructure, data factory, application center and security. The Company offers various solutions, including big data solutions; finance solutions; human resource solutions; marketing solutions; procurement solutions; sales solutions; systems, applications, products (SAP) solutions, and Microsoft solutions.",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
                {
                    Name = "Science In Sport Ltd",
                    Description ="Science in Sport plc is engaged in developing, manufacturing and marketing sports nutrition products for professional athletes and sports enthusiasts. The Company's product lines include SiS GO isotonic powders and gels, which are digestible carbohydrates for use during exercise; SiS hydration products, which include SiS GO Hydro tablets and SiS GO Electrolyte powders; SiS GO Bars, which include cereal-based food bars; SiS REGO range, which includes drinks and protein bars for recovery after training, and SiS Protein, which is a whey protein range for lean muscle development. The Company offers products in sport categories, including cycling, running, gym, team sports, triathlon and rowing. The Company's products include SiS GO Energy, SiS REGO Rapid Recovery, SiS WHEY20, SiS Whey Protein, SiS GO Isotonic Energy Gel, SiS Elite Team SKY and GO Energy Bar. The Company's subsidiaries include SiS (Science in Sport) Limited, SiS APAC Pty Limited and Science in Sport Inc.",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
                new Investment
                {
                    Name = "Pure Circle Limited",
                    Description ="PureCircle Limited is a producer of stevia ingredients for the global food and beverage industry. The Company focuses on encouraging healthier diets around the world through the supply of natural ingredients to the global food and beverage industry. The Company has over 40 stevia-related patents. The Company's Zeta Family ingredients consists of the sugar, such as steviol glycosides, including Reb M and Reb D, and allow for the deepest calorie reductions by food and beverage companies. The Company is engaged in production, marketing and distribution of natural sweeteners and flavors. The Company's geographical segments include Asia, Europe and Americas. The Company is also engaged in the production and marketing of stevia leaf extract. The Company involves in plant breeding, which includes Stevia varieties with sweet glycoside content; harvesting, which provides training to farmers; extraction; purification; application, and finished product. It has offices throughout the world.",
                    DesirabilityStatement = "",
                    InitialInvestment = 2,
                    Symbol = "$",
                    Value = 2,
                    ValueProposition = "",
                    Factors = new List<InvestmentInfluenceFactor_Investment>(),
                    Groups = new List<InvestmentGroup_Investment>()

                },
            });
            investments.ForEach(i =>
            {
                i.Factors = new List<InvestmentInfluenceFactor_Investment>();
                i.Groups = new List<InvestmentGroup_Investment>();
                i.Risks = new List<InvestmentRisk_Investment>();
                i.Regions = new List<Region_Investment>();
            });

            db.Investments.AddRange(investments);
            db.SaveChanges();
        }
    }


    public class Dbinitializer1 : IDbInitializer
    {
        public void Initialize(ApplicationDbContext db)
        {
            

            var MAX = 10;
            var factors = new List<InvestmentInfluenceFactor> {
                    new InvestmentInfluenceFactor {
                        Name = "Weather", Description = "The climate will affect the investment.",
                        Influence = "Sunny weather helps, rainy weather doesn't"},
                    new InvestmentInfluenceFactor {
                        Name = "Competiion", Description = "The competition dictates te supply and demand",
                        Influence = "The more cometition the less buiness you get if the competition or on par to you"},
            };
            String[] samples = new String[]
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
            foreach (String each in samples)
            {
                InvestmentInfluenceFactor f = new InvestmentInfluenceFactor { Name = each, Description = "description about " + each };
                f.Influence = "influenced by " + each;
                factors.Add(f);
            }
            factors.ForEach(f => f.Investments = new List<InvestmentInfluenceFactor_Investment>());
            factors.ForEach(f => db.Factors.Add(f));

            db.SaveChanges();

            // make some notes for factors
            var notes = factors.Select(f => new InvestmentNote { OwningEntityType = Common.EntityType.InvestmentInfluenceFactor, OwningEntityId = f.Id, Description = "Note contents for factor " + f.Name, Name = "Title" + f.Name });
            db.Notes.AddRange(notes);
            db.SaveChanges();

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
            groups.ForEach(g => db.Groups.Add(g));

            db.SaveChanges();

            // make some group notes
            List<InvestmentNote> group_notes = new List<InvestmentNote>();
            groups.ForEach(r => {
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


            var regions = new List<Region> {
                new Region { Name = "UK ALL COMPANIES (Fund Sector)" },
                new Region { Name = "UK SMALLER COMPANIES (Fund Sector)"},
                new Region { Name = "JAPAN (Fund Sector)"},
                new Region { Name = "JAPANESE SMALLER COMPANIES (Fund Sector)"},
                new Region { Name = "ASIA PACIFIC EXCLUDING JAPAN (Fund Sector)"},
                new Region { Name = "CHINA / GREATER CHINA SECTOR (Fund Sector)"},
                new Region { Name = "NORTH AMERICA (Fund Sector)"},
                new Region { Name = "NORTH AMERICAN SMALLER COMPANIES (Fund Sector)"},
                new Region { Name = "EUROPE INCLUDING UK (Fund Sector)"},
                new Region { Name = "EUROPE EXCLUDING UK (Fund Sector)"},
                new Region { Name = "EUROPEAN SMALLER COMPANIES (Fund Sector)"},
                new Region { Name = "GLOBAL (Fund Sector)"},
                new Region { Name = "GLOBAL EMERGING MARKETS (Fund Sector)" }
            };

            regions.ForEach(r => r.Investments = new List<Region_Investment>());
            regions.ForEach(r => db.Regions.Add(r));
            db.SaveChanges();

            // make some region notes
            List<InvestmentNote> region_notes = new List<InvestmentNote>();
            regions.ForEach(r => {
                region_notes.Add(new InvestmentNote
                {
                    Name = "note for risk " + r.Name,
                    Description = "description for " + r.Description,
                    OwningEntityId = r.Id,
                    OwningEntityType = Common.EntityType.Region
                });
            });

            db.Notes.AddRange(region_notes);
            db.SaveChanges();

            var risks = new List<InvestmentRisk> {
                new InvestmentRisk { Name = "Director dismissal", Description = "Financial officer fired due to corruption", Type = Common.RiskType.Company },
                new InvestmentRisk { Name = "Competition", Description = "Competition from other companies", Type = Common.RiskType.Company },
                new InvestmentRisk { Name = "Fashion", Description = "Fashion/popularity of the comodity", Type = Common.RiskType.Company },
                new InvestmentRisk { Name = "Earnings report", Description = "Investor perception based on earnings", Type = Common.RiskType.Company },
            };

            risks.ForEach(r => r.Investments = new List<InvestmentRisk_Investment>());
            risks.ForEach(r => db.Risks.Add(r));
            db.SaveChanges();

            // make some risk notes
            List<InvestmentNote> risk_notes = new List<InvestmentNote>();
            risks.ForEach(r => {
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

            var investments = new List<Investment>();
            for (int i = 0; i < MAX; i++)
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

                int gmax = gmax = new Random(DateTime.Now.Millisecond).Next(1, groups.Count);

                for (int g = 0; g < gmax; g++)
                {
                    InvestmentGroup_Investment link = new InvestmentGroup_Investment
                    {
                        Investment = investment,
                        InvestmentGroup = groups[g]
                    };
                    investment.Groups.Add(link);
                }

                int fmax = new Random(DateTime.Now.Millisecond).Next(1, factors.Count);

                for (int f = 0; f < fmax; f++)
                {
                    InvestmentInfluenceFactor_Investment link = new InvestmentInfluenceFactor_Investment
                    {
                        Investment = investment,
                        InvestmentInfluenceFactor = factors[f]
                    };
                    investment.Factors.Add(link);
                }

                int rmax = new Random(DateTime.Now.Millisecond).Next(1, risks.Count);

                for (int r = 0; r < rmax; r++)
                {
                    InvestmentRisk_Investment link = new InvestmentRisk_Investment
                    {
                        Investment = investment,
                        InvestmentRisk = risks[r]
                    };
                    investment.Risks.Add(link);
                }

                int regionmax = new Random(DateTime.Now.Millisecond).Next(1, regions.Count);
                for (int r = 0; r < rmax; r++)
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

            db.Notes.AddRange(investments.Select(m => new InvestmentNote { OwningEntityType = Common.EntityType.Investment, OwningEntityId = m.Id, Name = "note for " + m.Name, Description = "note for " + m.Name }));
            db.SaveChanges();
        }
    }
}