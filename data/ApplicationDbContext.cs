using ASWebEssentials.Models;
using ASWebEssentials.Models.CartModels;
using ASWebEssentials.Models.ContactModel;
using ASWebEssentials.Models.OrderModels;
using ASWebEssentials.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASWebEssentials.Data

{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
		public DbSet<Product> Products { get; set; }
		public DbSet<Option> Options { get; set; }
		public DbSet<Category> Categorys { get; set; }
		public	DbSet<Service> Service { get; set; }
		public DbSet<Services> Services { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketRes> TicketRes { get; set; }
        public DbSet<AdminMess> AdminMess { get; set; }
        public DbSet<MessageRes> MessageRes { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        { 
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Data seeding logic
			SeedData(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private void SeedData(ModelBuilder modelBuilder)
		{
			Product product1 = new Product() { 
				Id = 1, 
				ProdName = "Logo design",
				ShortDesc = "A logo serves as the face of your business and helps your audience recognize who you are and what you stand for.",
				DetailDesc = "A distinctive and memorable logo is the cornerstone of a company's visual identity. It should reflect the brand's personality, values, and industry. Logos can be wordmarks, lettermarks, symbols, or a combination.",
				Features = new List<string?>
						{"Simplicity: Clarity in the logo's structure helps convey the intended message quickly.",
						"Memorability: A successful logo should be easily remembered.",
						"Versatility: A versatile logo works well across different mediums and sizes."},
				ImageUrl = "https://www.designfreelogoonline.com/wp-content/uploads/2022/01/BEST-LOGO-IDEAS.jpg"
            };
			Product product2 = new Product()
			{
				Id = 2,
				ProdName = "Marketing Materials",
				ShortDesc = "Brochures, flyers, and posters with visually appealing graphics can effectively convey information about products or services.",
				DetailDesc = "Various Sizes and Formats: Provide flexibility in terms of sizes and formats to accommodate different marketing needs (e.g., brochures, flyers, posters).",
				Features = new List<string?>
						{"Various Sizes and Formats: Provide flexibility in terms of sizes and formats to accommodate different marketing needs (e.g., brochures, flyers, posters).",
						"Integrate QR codes into marketing materials to facilitate easy access to digital content or websites.",},

				ImageUrl = "https://proprintingnp.files.wordpress.com/2017/06/corporate_identity.png"
			};
            Product product3 = new Product()
            {
                Id = 3,
                ProdName = "Content Writing",
                ShortDesc = "A content writing service for a company involves creating, developing, and delivering written content that effectively communicates the company's messages, values, and information to its target audience. ",
                DetailDesc = "A well-executed content writing service plays a crucial role in building brand credibility, engaging with the target audience, and driving business growth. It helps companies establish a strong online presence and effectively communicate their messages to both existing and potential customers",
                Features = new List<string?>
                        {"Website Content:Crafting engaging and informative content for the company's website, including homepage content, about us pages, product or service descriptions, and contact information.\r\nBlog Posts:",
                        "Writing regular blog posts to share industry insights, company news, and valuable information that attracts and retains a target audience. ",
                        "Product Descriptions: Writing persuasive and informative product or service descriptions that highlight features, benefits, and unique selling points."},

                ImageUrl = "https://contentwriters.com/blog/wp-content/uploads/PPC2018.jpg"
            };

            modelBuilder.Entity<Product>().HasData(product1, product2, product3);

			Option option1 = new Option()
			{
				Id = 1,
				ProductId = 1,
				OptionName = "Monogram logo",
				Price = 10,
				ImageUrl = "https://cdn.logojoy.com/wp-content/uploads/2019/03/26162703/4x4_famous_monogram_logos.png",
                OptDesc = "A monogram logo is a design consisting of one or more letters, typically initials, that are intertwined, overlapped, or combined in a visually appealing way to form a single symbol.",
            };
			Option option2 = new Option()
			{
				Id = 2,
				ProductId = 1,
				OptionName = "Abstract logo",
				Price = 20,
				ImageUrl = "https://static.vecteezy.com/system/resources/previews/002/214/980/original/abstract-logo-design-vector.jpg",
                OptDesc = "Abstract logos are designs that convey the essence of a brand or concept through non-literal, symbolic, or artistic elements. ",
            };
			Option option3 = new Option()
			{
				Id = 3,
				ProductId = 1,
				OptionName = "Wordmark logos",
				Price = 30,
				ImageUrl = "https://cdn.sanity.io/images/kts928pd/production/b0f5b4380c820d0db1203812389986f27ca459ed-1400x838.png",
                OptDesc = "Wordmark logos, also known as logotypes, are typographic designs that consist of the company or brand name in a stylized font or typography.",
            };
			Option option4 = new Option()
			{
				Id = 4,
				ProductId = 2,
				OptionName = "Brochures",
				Price = 10,
				ImageUrl = "https://atd-blog.s3.us-east-2.amazonaws.com/wp-content/uploads/2023/05/01082450/2-47.webp",
                OptDesc = "Brochures are versatile marketing materials used to convey information about a company, product, service, or event. They typically consist of multiple pages folded together and are distributed to potential customers or clients. ",
            };
			Option option5 = new Option()
			{
				Id = 5,
				ProductId = 2,
				OptionName = "Flyers",
				Price = 20,
				ImageUrl = "https://brandpacks.com/wp-content/uploads/edd/2020/12/qr-code-flyer-templates-5x7-Preview1.jpg",
                OptDesc = "Our flyer design services are crafted with a keen eye for professionalism and sophistication. We understand the importance of making a strong visual impact that reflects positively on your brand or event. With years of experience in graphic design, we specialize in creating flyers that not only catch the eye but also communicate your message effectively.",
            };
            Option option6 = new Option()
            {
                Id = 6,
                ProductId = 3,
                OptionName = "Blog Posts",
                Price = 20,
                ImageUrl = "https://www.fhoke.com/wp-content/uploads/2023/08/10-Best-Blog-Designs.webp",
                OptDesc = "Informative, engaging articles published on a blog, covering a wide range of topics and interests.",
            };
            Option option7 = new Option()
            {
                Id = 7,
                ProductId = 3,
                OptionName = "Product Descriptions",
                Price = 30,
                ImageUrl = "https://ceblog.s3.amazonaws.com/wp-content/uploads/2022/11/15194057/6-Examples-of-Product-Descriptions-6.png",
                OptDesc = "Compelling descriptions of products or services, highlighting features, benefits, and unique selling points.",
            };
            Option option8 = new Option()
            {
                Id = 8,
                OptionName = "Website Content",
                Price = 50,
                ImageUrl = "https://creativecopywritingsg.com/wp-content/uploads/2023/08/Creative-Copywriting-Killer-website-content.png",
                OptDesc = "Informative, persuasive content that describes a company, its products, services, and value proposition, designed to engage visitors and convert them into customers.",
            };

            modelBuilder.Entity<Option>().HasData(option1, option2, option3, option4, option5, option6, option7, option8);

			Category category1 = new Category()
			{
				Id = 1,
				CatName = "Graphic Design",
			};
			Category category2 = new Category()
			{
				Id = 2,
				CatName = "Content Creation",
			};
            Category category3 = new Category()
            {
                Id = 3,
                CatName = "Advertising and Promotion ",
            };

            modelBuilder.Entity<Category>().HasData(category1, category2, category3);

			modelBuilder.Entity("CategoryProduct").HasData(
				new { CategoriesId = 1, ProductsId = 1 },
                new { CategoriesId = 1, ProductsId = 2 },
                new { CategoriesId = 2, ProductsId = 3 },
                new { CategoriesId = 3, ProductsId = 1 },
                new { CategoriesId = 3, ProductsId = 2 },
                new { CategoriesId = 3, ProductsId = 3 });

			Service service1 = new Service()
			{
                Id = 1,
                Name = "Website Design and Development",
                Description = "At AS Web Essentials, we specialize in crafting bespoke websites that seamlessly align with our clients' unique visions and business objectives. Our commitment to excellence in design and functionality ensures that your online presence stands out amidst the digital landscape.",
                KeyOfferings = new List<string> { "Customized Solutions: We don't believe in one-size-fits-all. Our team works closely with each client to understand their goals and requirements, delivering tailor-made solutions that reflect their brand identity.",
												  "Responsive Web Design: In a world where users access websites from a myriad of devices, our commitment to responsive design ensures a flawless user experience across desktops, tablets, and smartphones." },
                WhyChooseUs = new List<string> { "Innovative Designs: Our team of creative designers brings innovation to the forefront, ensuring that your website not only meets industry standards but also sets a new benchmark for aesthetics and user engagement.",
                                                "Cutting-Edge Technologies: Leveraging the latest technologies and frameworks, including [mention specific technologies, e.g., React, Vue.js, Bootstrap, ASP.NET MVC, and C#], we bring your vision to life with robust and scalable solutions.",
                                                "User-Centric Approach: Putting the user experience first, we design and develop interfaces that are not only visually appealing but also intuitive, making navigation a seamless and enjoyable experience."},
                OurProcess = new List<string> { "Discovery: We start by understanding your business, target audience, and goals to lay the foundation for a successful project.",
                                                "Design: Our creative team conceptualizes designs that blend innovation with functionality, providing you with mockups for feedback and approval.",
                                                "Development: Using cutting-edge technologies, we bring the approved design to life, ensuring a seamless and responsive user experience.",
                                                "Testing: Rigorous testing is conducted to guarantee the functionality, compatibility, and security of your website across various devices and browsers.",
                                                "Delivery: Your custom website is delivered on time, ready to make a powerful impact in the online realm."},
                GetInTouch = "Ready to elevate your online presence? Contact us today for a consultation and let's embark on a journey to bring your vision to life.",
            };
            Service service2 = new Service()
            {
                Id = 2,
                Name = "E-Commerce Development",
                Description = "At AS Web Essentials, we specialize in transforming online aspirations into thriving e-commerce ventures. Our E-Commerce Development services focus on delivering secure, user-friendly, and feature-rich online stores that drive conversions and enhance the overall shopping experience.",
                KeyOfferings = new List<string> { "Online Store Creation: We excel in building captivating online stores that not only showcase your products but also provide a seamless and enjoyable shopping journey for your customers.",
                                                  "Secure Payment Gateways: Security is paramount. Our e-commerce solutions include the integration of robust and secure payment gateways, ensuring safe and reliable transactions for your customers.",
                                                  "Inventory Management: Stay in control of your products with our comprehensive inventory management systems. Track stock levels, manage variations, and streamline your product catalog effortlessly.",
                                                  "Order Processing Systems: Our solutions encompass efficient order processing systems, from order placement to fulfillment, ensuring a smooth and organized workflow for your business." },
                WhyChooseUs = new List<string> { "E-Commerce Expertise: With a proven track record in e-commerce development, we bring a wealth of expertise to the table, ensuring your online store is both visually appealing and functionally robust.",
                                                "Scalable Solutions: As your business grows, our e-commerce solutions grow with you. We provide scalable platforms that can adapt to your evolving needs and expanding product offerings.",
                                                "User-Focused Design: Understanding the importance of user experience in e-commerce, we design interfaces that are intuitive, easy to navigate, and optimized for conversions." },
                OurProcess = new List<string> { "Consultation: We start by understanding your business goals, target audience, and unique requirements to tailor a solution that aligns with your vision.",
                                                "Design and Development: Our creative and development teams collaborate to create an online store that not only meets but exceeds your expectations.",
                                                "Payment Gateway Integration: Security is a priority. We integrate secure payment gateways that instill confidence in your customers and protect their sensitive information.",
                                                "Inventory and Order Systems: Implementing robust inventory management and order processing systems to streamline your e-commerce operations." },
                GetInTouch = "Ready to take your products online? Contact us today for a consultation and let's turn your e-commerce vision into reality.",
            };
            Service service3 = new Service()
            {
                Id = 3,
                Name = "Content Management Systems (CMS)",
                Description = "At AS Web Essentials, we empower businesses with robust and scalable websites through our expertise in Content Management Systems (CMS). Whether you are seeking a dynamic WordPress site, a versatile Drupal platform, or a feature-rich Joomla solution, our CMS development services ensure your online presence is both flexible and tailored to your unique needs.",
                KeyOfferings = new List<string> { "CMS Website Development: We specialize in developing websites on popular CMS platforms such as WordPress, Drupal, and Joomla, providing a foundation for dynamic, easily maintainable, and scalable online experiences.",
                                                  "Customization and Extension: Tailoring off-the-shelf CMS solutions to meet your specific requirements, we go beyond the basics, customizing and extending functionality to match your brand's identity and business goals." },
                WhyChooseUs = new List<string> { "Versatility in CMS Platforms: Our team is well-versed in a variety of CMS platforms, allowing us to recommend and implement the right solution based on your unique needs and preferences.",
                                                "Scalable Solutions: Our CMS solutions are designed for scalability, ensuring that your website can grow alongside your business, accommodating new features and content seamlessly.",
                                                "User-Friendly Interfaces: We prioritize user experience, creating intuitive back-end interfaces that empower you to manage your content effortlessly." },
                OurProcess = new List<string> { "Needs Assessment: We start by understanding your business requirements, content strategy, and long-term goals to recommend the most suitable CMS platform.",
                                                 "Platform Selection: Based on our assessment, we guide you in selecting the CMS platform that aligns with your specific needs and preferences.",
                                                 "Development and Customization: Our development team brings your vision to life by building a website on the chosen CMS platform, tailoring it to your unique requirements.",
                                                 "Training and Support: We provide training to ensure your team is comfortable managing content on the CMS, and ongoing support to address any questions or concerns." },
                GetInTouch = "Ready to streamline your content management? Contact us today for a consultation and let's build a dynamic and scalable online presence together.",
            };
            Service service4 = new Service()
            {
                Id = 4,
                Name = "Web Application Development",
                Description = "Transform your vision into reality with our Web Application Development services. We specialize in creating dynamic and interactive web applications that provide a superior user experience while ensuring a robust and efficient backend.",
                KeyOfferings = new List<string> { "Front-End Excellence: Build captivating user interfaces with front-end frameworks like React or Vue.js, creating seamless and engaging experiences for your audience.",
                                                   "Responsive Design Mastery: Enhance user experience through responsive design elements powered by Bootstrap. Our designs ensure your application is visually appealing and functional across a variety of devices.",
                                                   "Powerful Backend Solutions: On the backend, we develop high-performance systems utilizing cutting-edge technologies such as Node.js. Additionally, we offer comprehensive solutions supplemented with ASP.NET MVC and C#, ensuring efficiency and scalability." },
                WhyChooseUs = new List<string> { "Unified Approach: Our integrated approach harmonizes front-end and back-end development, delivering web applications that are not only visually stunning but also functionally robust.",
                                                "Technological Expertise: Leverage our expertise in a spectrum of technologies, from React and Vue.js for front-end to Node.js, ASP.NET MVC, and C# for powerful backend solutions.",
                                                "Scalability and Efficiency: Future-proof your web application with our focus on scalability and efficiency, accommodating growth and evolving needs without compromising performance." },
                OurProcess = new List<string> { "Ideation and Planning: Collaborative sessions to understand your vision, goals, and user needs, laying the groundwork for a successful project.",
                                                "Front-End Development: Craft visually appealing and intuitive user interfaces using React or Vue.js, ensuring an immersive user experience.",
                                                "Responsive Design Implementation: Utilize Bootstrap to implement responsive design elements, ensuring your web application looks and performs seamlessly across devices.",
                                                "Backend Development: Develop powerful backend systems using technologies such as Node.js, and optionally supplement with ASP.NET MVC and C# for comprehensive solutions.",
                                                "Testing and Optimization: Rigorous testing to ensure reliability, security, and optimal performance, guaranteeing a flawless user experience." },
                GetInTouch = "Ready to embark on your web application journey? Contact us for a consultation, and let's bring your ideas to life in the digital realm.",
            };
            Service service5 = new Service()
            {
                Id = 5,
                Name = "UI/UX Design",
                Description = "Immerse your audience in an exceptional digital experience with our UI/UX Design services. We specialize in crafting visually stunning and user-friendly interfaces, backed by meticulous user research and usability testing.",
                KeyOfferings = new List<string> { "Visually Appealing Interfaces: Elevate your brand with visually captivating interfaces that not only meet but exceed user expectations. Our designs are crafted to leave a lasting impression.",
                                                "User-Friendly Design: Prioritize user experience with intuitive and user-friendly designs. We focus on creating interfaces that are easy to navigate, enhancing user engagement and satisfaction.",
                                                "User Research: Dive deep into user behavior, preferences, and expectations through comprehensive user research. This forms the foundation for designing solutions that resonate with your target audience.",
                                                "Usability Testing: Validate and refine our designs through rigorous usability testing. This iterative process ensures that the final product is not only visually appealing but also functionally seamless." },
                WhyChooseUs = new List<string> { "Design Excellence: Our design team brings creativity and expertise to every project, ensuring your interfaces are not just beautiful but also align with your brand identity and user needs.",
                                                "User-Centric Approach: We put your users at the center of the design process, crafting interfaces that enhance their experience and contribute to the overall success of your digital presence.",
                                                "Data-Driven Decisions: Utilize insights from user research and usability testing to make informed design decisions, ensuring your interfaces are optimized for usability and engagement." },
                OurProcess = new List<string> { "Discovery Phase: Collaborate with stakeholders to understand your brand, target audience, and project goals, setting the stage for a successful design journey.",
                                                 "User Research and Persona Development: Conduct in-depth user research to create personas, helping us empathize with your audience and design solutions tailored to their needs.",
                                                 "Design Prototyping: Bring concepts to life through interactive prototypes, allowing you to visualize the user journey and provide valuable feedback.",
                                                 "Usability Testing: Conduct usability testing on prototypes, refining designs based on user feedback to ensure optimal functionality and satisfaction.",
                                                 "Final Design Delivery: Deliver polished and user-approved designs, ready for seamless integration into your digital platforms."},
                GetInTouch = "Ready to redefine your digital presence? Contact us for a consultation, and let's create visually stunning and user-friendly interfaces that leave a lasting impact. ",
            };
            Service service6 = new Service()
            {
                Id = 6,
                Name = "Custom Software Development",
                Description = "Empower your business with our Custom Software Development services, where we specialize in building tailored solutions designed to address your unique business needs. Beyond development, we offer ongoing support and maintenance to ensure your software evolves seamlessly with your business.",
                KeyOfferings = new List<string> { "Bespoke Software Solutions: We excel in crafting custom software solutions that align precisely with your business requirements. From automation to optimization, our solutions are tailored to enhance your operational efficiency.",
                                                  "Tailored to Your Needs: No two businesses are the same, and neither should their software be. Our approach involves understanding your processes, challenges, and goals to deliver a solution perfectly aligned with your needs.",
                                                  "Ongoing Support and Maintenance: Beyond development, we provide continuous support and maintenance to keep your custom software running smoothly. Our team ensures that your software remains up-to-date, secure, and responsive to changing business demands." },
                WhyChooseUs = new List<string> { "Industry Expertise: With a deep understanding of various industries, we bring expertise to the table, ensuring your custom software addresses specific challenges within your business sector.",
                                                 "Scalable Solutions: Our custom software solutions are designed with scalability in mind, accommodating your business growth and evolving requirements without compromising performance.",
                                                 "Proactive Support: Enjoy peace of mind with our proactive support and maintenance services. We identify and resolve issues before they impact your operations, minimizing downtime." },
                OurProcess = new List<string> { "Requirement Analysis: We start by delving into your business processes, goals, and challenges to define the scope and requirements for your custom software. ",
                                                 "Development and Customization: Leveraging industry-leading technologies, we build a bespoke solution tailored to your unique specifications, ensuring seamless integration into your workflow.",
                                                 "Testing and Quality Assurance: Rigorous testing is conducted to guarantee the functionality, security, and performance of your custom software, ensuring it meets the highest standards.",
                                                 "Deployment: We deploy the custom software in your environment, providing any necessary training to ensure a smooth transition.",
                                                 "Ongoing Support and Maintenance: Our commitment extends beyond deployment. We offer continuous support, monitoring, and maintenance to keep your software optimized and secure." },
                GetInTouch = "Ready to elevate your operations with custom software? Contact us for a consultation, and let's build a solution that transforms the way you do business. ",
            };
            Service service7 = new Service()
            {
                Id = 7,
                Name = "Search Engine Optimization (SEO)",
                Description = "Boost your online visibility and drive organic traffic with our SEO services. We implement industry-leading practices to optimize your website for search engines, conducting comprehensive keyword research, on-page optimization, and strategic link building.",
                KeyOfferings = new List<string> { "SEO Best Practices: We employ the latest SEO best practices to enhance your website's visibility on search engines, ensuring it ranks higher for relevant search queries.",
                                                  "Keyword Research: Uncover the language your audience uses. Our keyword research identifies the terms and phrases potential customers are searching for, laying the groundwork for effective optimization.",
                                                  "On-Page Optimization: Enhance your website's structure, content, and meta tags for search engine algorithms. Our on-page optimization ensures that each page communicates its relevance and value effectively.",
                                                  "Link Building Strategies: Establish a robust online presence through strategic link building. We cultivate quality backlinks to your website, improving its authority and credibility in the eyes of search engines."},
                WhyChooseUs = new List<string> { "Proven Results: Benefit from our track record of delivering tangible results through effective SEO strategies. We focus on driving targeted traffic that converts into meaningful engagement and leads.",
                                                 "Transparent Reporting: Stay informed with transparent reporting on key performance indicators, including keyword rankings, traffic growth, and conversion rates.",
                                                 "Adaptability to Algorithms: Our team stays abreast of search engine algorithm changes, adapting strategies to ensure your website maintains optimal visibility and compliance with evolving standards." },
                OurProcess = new List<string> { "Website Audit: We conduct a comprehensive audit of your website to identify strengths, weaknesses, and opportunities for improvement.",
                                                 "Keyword Research and Strategy: Through in-depth keyword research, we develop a strategic plan to target the terms that matter most to your audience.",
                                                 "On-Page Optimization: Implementing changes to your website's structure, content, and meta tags to optimize for relevant keywords.",
                                                 "Link Building Campaigns: Strategically acquiring quality backlinks from reputable sources to enhance your website's authority.",
                                                 "Monitoring and Reporting: Regular monitoring of SEO performance metrics, coupled with transparent reporting, allows us to fine-tune strategies for continuous improvement." },
                GetInTouch = "Ready to climb the ranks in search engine results? Contact us for a consultation, and let's propel your website to new heights with effective SEO strategies.",
            };
            modelBuilder.Entity<Service>().HasData(service1, service2, service3, service4, service5, service6, service7);

            Services ser1 = new Services()
            {
                Id = 1,
                Name = "Website Design and Development",
                Descr1 = "Creating custom websites tailored to the client's needs.",
                Descr2 = "Responsive web design to ensure compatibility across various devices.",
                Image = "https://www.visualmarketing.com.au/wp-media/Dollarphotoclub_76030506.jpg",
                ServiceId = 1,
            };
            Services ser2 = new Services()
            {
                Id = 2,
                Name = "E-Commerce Development",
                Descr1 = "Building online stores with secure payment gateways.",
                Descr2 = "Integrating inventory management and order processing systems.",
                Image = "https://accedor.com/wp-content/uploads/2020/12/eCommerce-800x579.jpg",
                ServiceId = 2,
            };
            Services ser3 = new Services()
            {
                Id = 3,
                Name = "Content Management Systems (CMS)",
                Descr1 = "Developing websites using popular CMS platforms like WordPress.",
                Descr2 = "Customizing and extending CMS functionality.",
                Image = "https://cdn.hostadvice.com/2023/04/final-what-is-a-cms-2.png",
                ServiceId = 3,
            };
            Services ser4 = new Services()
            {
                Id = 4,
                Name = "Web Application Development",
                Descr1 = "Develop dynamic web apps using React or Vue.js, and enhance UX with Bootstrap's responsive design.",
                Descr2 = "Backend development with Node.js or ASP.NET MVC/C# for robust solutions.",
                Image = "https://www.usnews.com/dims4/USNEWS/fb6e5fb/2147483647/crop/2000x1334+0+2/resize/970x647/quality/85/?url=https%3A%2F%2Fwww.usnews.com%2Fcmsmedia%2F65%2F62%2Fc9cb60d24ac89d56462b1228574a%2F201009-codingcomputer-stock.jpg",
                ServiceId = 4,
            };
            Services ser5 = new Services()
            {
                Id = 5,
                Name = "UI/UX Design",
                Descr1 = "Creating visually appealing and user-friendly interfaces.",
                Descr2 = "Conducting user research and usability testing.",
                Image = "https://moringaschool.com/wp-content/uploads/2023/03/product-design.jpg",
                ServiceId = 5,
            };
            Services ser6 = new Services()
            {
                Id = 6,
                Name = "Custom Software Development",
                Descr1 = "Building bespoke software solutions tailored to specific business needs.",
                Descr2 = "Providing ongoing support and maintenance for custom software.",
                Image = "https://media.licdn.com/dms/image/D4D12AQGVEnLLCAIgYg/article-cover_image-shrink_720_1280/0/1689851568571?e=2147483647&v=beta&t=qPJeL6UNNG8MettEVK3eN27n9vcsTJcvTaH8YKfL52M",
                ServiceId = 6,
            };
            Services ser7 = new Services()
            {
                Id = 7,
                Name = "Search Engine Optimization (SEO)",
                Descr1 = "Implementing SEO best practices to improve website visibility in search engines.",
                Descr2 = "Conducting keyword research, on-page optimization, and link building.",
                Image = "https://www.dodwellsolutions.com/wp-content/uploads/2020/10/seo-search-engine-optimization.png",
                ServiceId = 7,
            };

            modelBuilder.Entity<Services>().HasData(ser1, ser2, ser3, ser4, ser5, ser6, ser7);

            Feature feature1 = new Feature()
            {
                Id = 1 ,
                Name = "Customized Solutions",
                Description = "Tailor-made websites crafted to meet your unique business needs and goals.",
            };
            Feature feature2 = new Feature()
            {
                Id = 2,
                Name = "Cutting-Edge Technology",
                Description = "We leverage the latest tools and frameworks to ensure your website is modern, efficient, and future-proof.",
            };
            Feature feature3 = new Feature()
            {
                Id = 3,
                Name = "Innovative Designs",
                Description = "Creative and visually appealing designs that captivate your audience and leave a lasting impression.",
            };
            Feature feature4 = new Feature()
            {
                Id = 4,
                Name = "Mobile-First Approach",
                Description = "Responsive designs that provide seamless user experiences across all devices, from desktops to smartphones.",
            };
            Feature feature5 = new Feature()
            {
                Id = 5,
                Name = "User-Centric Design",
                Description = "Intuitive layouts and interfaces designed with your users in mind, enhancing overall usability and satisfaction.",
            };
            Feature feature6 = new Feature()
            {
                Id = 6,
                Name = "E-commerce Expertise",
                Description = "Expertise in developing secure and user-friendly e-commerce solutions that drive sales and conversions.",
            };
            Feature feature7 = new Feature()
            {
                Id = 7,
                Name = "Agile Development Methodology",
                Description = "Agile development practices ensure flexibility, efficiency, and adaptability throughout the project lifecycle.",
            };
            Feature feature8 = new Feature()
            {
                Id = 8,
                Name = "SEO Optimization",
                Description = "Implementing SEO best practices to improve your website's visibility and drive organic traffic.",
            };
            Feature feature9 = new Feature()
            {
                Id = 9,
                Name = "Security Measures",
                Description = "Robust security measures to protect your data and provide a safe browsing experience for your users.",
            };
            Feature feature10 = new Feature()
            {
                Id = 10,
                Name = "Fast Loading Times",
                Description = "Optimized performance for fast loading times, reducing bounce rates and improving user satisfaction.",
            };
            Feature feature11 = new Feature()
            {
                Id = 11,
                Name = "Transparent Communication",
                Description = "Clear and transparent communication throughout the development process, keeping you informed every step of the way.",
            };
            Feature feature12 = new Feature()
            {
                Id = 12,
                Name = "Scalability",
                Description = "Scalable solutions that grow with your business, ensuring your website can adapt to changing needs and demands",
            };
            Feature feature13 = new Feature()
            {
                Id = 13,
                Name = "Cross-Browser Compatibility",
                Description = "Compatibility with all major web browsers, ensuring a consistent experience for users across different platforms",
            };
            Feature feature14 = new Feature()
            {
                Id = 14,
                Name = "Ongoing Support and Maintenance",
                Description = "Dedicated support and maintenance services to keep your website running smoothly and efficiently.",
            };
            Feature feature15 = new Feature()
            {
                Id = 15,
                Name = "Collaborative Approach",
                Description = "We work closely with you to understand your vision and goals, ensuring the final product exceeds your expectations",
            };

            modelBuilder.Entity<Feature>().HasData(feature1, feature2, feature3, feature4, feature5, feature6, feature7, feature8, feature9, feature10, feature11, feature12, feature13, feature14, feature15);
        }
    }
	}
