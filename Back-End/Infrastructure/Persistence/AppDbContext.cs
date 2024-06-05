using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }

        public AppDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.ProductId);
                entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.Discount);
                entity.Property(p => p.ImageUrl).IsRequired();

                entity.HasOne<Category>(ca => ca.category)
                .WithMany(ad => ad.products)
                .HasForeignKey(c => c.Category);
            }
            );

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.CategoryId).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).HasMaxLength(100);

                entity.HasMany(c => c.products)
                .WithOne(p => p.category);
            }
            );

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");
                entity.HasKey(s => s.SaleId);
                entity.Property(s => s.SaleId).ValueGeneratedOnAdd();
                entity.Property(s => s.TotalPay).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.Subtotal).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.TotalDiscount).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.Taxes).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(s => s.Date).IsRequired();

                entity.HasMany(s => s.SaleProducts)
                .WithOne(sp => sp.sale);
            }
            );

            modelBuilder.Entity<SaleProduct>(entity =>
            {
                entity.ToTable("SaleProduct");
                entity.HasKey(e => e.ShoppingCartId);
                entity.Property(t => t.ShoppingCartId).ValueGeneratedOnAdd();
                entity.Property(sp => sp.Sale).IsRequired();
                entity.Property(sp => sp.Product).IsRequired();
                entity.Property(sp => sp.Quantity).IsRequired();
                entity.Property(sp => sp.Price).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(sp => sp.Discount);


                entity
                    .HasOne<Sale>(sp => sp.sale)
                    .WithMany(s => s.SaleProducts)
                    .HasForeignKey(sp => sp.Sale);

                entity
                    .HasOne<Product>(sp => sp.product)
                    .WithMany(s => s.SaleProducts)
                    .HasForeignKey(sp => sp.Product);
            }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electrodomesticos", },
                new Category { CategoryId = 2, Name = "Baño", },
                new Category { CategoryId = 3, Name = "Herramientas", },
                new Category { CategoryId = 4, Name = "Automotor", },
                new Category { CategoryId = 5, Name = "Electricidad", },
                new Category { CategoryId = 6, Name = "Plomeria", },
                new Category { CategoryId = 7, Name = "Cocina", },
                new Category { CategoryId = 8, Name = "Muebleria", },
                new Category { CategoryId = 9, Name = "Tecnologia", },
                new Category { CategoryId = 10, Name = "Jardin", }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Aire Acondicionado Nex On Off 2752fn",
                    Description = "Climatizar tus espacios a lo largo del año es sin duda algo importante para tu comodidad y la de tus seres queridos. Contar con un aire acondicionado con función frío/calor es la mejor decisión. Con este aire Nex conseguí una mejor relación costo-beneficio.",
                    Price = 850000m,
                    Category = 1,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/365635-1200-1200?v=638350437287400000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Ventilador Industrial 18 Metal Pioneer",
                    Description = "El ventilador PIONEER es mas econmico ya que ahorra un 50% de energia electrica\r\nEs practico, facil de trasladar y adaptabilidad.\r\nSus aspas Metalicas lo hacen un producto de gran durabilidad.\r\nsu base de apoyo firme y su rejilla protectora metalica lo hacen un producto seguro",
                    Price = 54996m,
                    Category = 1,
                    Discount = 12,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/371560-1200-1200?v=638422437614700000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Heladera Eslabon de Lujo Erd29Ab 273 L",
                    Description = "Esta heladera cuenta con vidrio templado en todos los estantes, ofreciendo calidad y resistencia. Los estantes son flexibles con altura regulable y cómodos anaqueles desmontables en la puerta.Los estantes se pueden mover en altura para optimizar el espacio, en función de las necesidades de guardado.",
                    Price = 530000m,
                    Category = 1,
                    Discount = 16,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/369370-1200-1200?v=638392835522730000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Lavarropas Drean Eco Next 6Kg Blanco",
                    Description = "Marca lider del mercado - 6 KG - 800 RPM - Eficiencia energetica A+. Color blanco.",
                    Price = 54996m,
                    Category = 1,
                    Discount = 5,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/372022-1200-1200?v=638428189685100000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Inodoro Two Piece con Depósito y Asiento",
                    Description = "El sanitario 2 piezas Hervas Blanco es la elección perfecta para tu baño. Con su diseño elegante y moderno, este sanitario no solo cumple con su función principal, sino que también agrega un toque de estilo a tu espacio.",
                    Price = 141700m,
                    Category = 2,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/325325-1200-1200?v=637928028976170000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Juego De Ducha Embutida Con Transferencia Cipres 103 - N2 Cromo",
                    Description = "Este juego de baño con tapa de ciprés 103/N2 CR es la elección perfecta para darle un toque de elegancia y estilo a tu baño.",
                    Price = 58996m,
                    Category = 2,
                    Discount = 25,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/356354-1200-1200?v=638223661536300000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Jabonera Cromo Andez Klub",
                    Description = "Bienvenido al mundo de la elegancia y la funcionalidad para tu baño. Nuestros accesorios están diseñados pensando en la excelencia en cada detalle. Con un enfoque meticuloso en la calidad y la durabilidad, estos accesorios están destinados a transformar tu espacio de baño en un oasis de comodidad y estilo.",
                    Price = 17000m,
                    Category = 2,
                    Discount = 12,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/375419-1200-1200?v=638478528895830000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Set de accesorios de baño Vessanti Marayui 5 piezas",
                    Description = "set acc. de baño 5 piezas marayui imp",
                    Price = 29000m,
                    Category = 2,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/340426-1200-1200?v=638085454583770000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Taladro Atornillador Percutor Bosch Gsb 18V-50",
                    Description = "El Taladro Perc Brushless 18V + 2 Bat de Easy es la herramienta perfecta para llevar a cabo tus proyectos de bricolaje con facilidad y eficiencia.",
                    Price = 842000m,
                    Category = 3,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/324566-1200-1200?v=637915901462570000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Amoladora Bosch Gws 700 + 5 Discos 220V",
                    Description = "La amoladora Bosch GWS 700 es la herramienta perfecta para aquellos que buscan potencia y precisión en sus proyectos de bricolaje. Con su motor de 220V, esta amoladora es capaz de realizar cortes y pulidos de alta calidad en una amplia variedad de materiales. Además, incluye 5 discos adicionales, lo que te permite comenzar a trabajar de inmediato sin tener que preocuparte por comprar accesorios adicionales.",
                    Price = 126000m,
                    Category = 3,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/367406-1200-1200?v=638357448136970000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Hormigonera Compacta 130 L",
                    Description = "La Hormigonera Deper 150 LTS es la compañera perfecta para tus proyectos de construcción.\r\n\r\nCon su capacidad de 150 litros, podrás mezclar grandes cantidades de hormigón de manera eficiente y rápida.",
                    Price = 464550m,
                    Category = 3,
                    Discount = 20,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/273755-1200-1200?v=637651577792600000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Sierra Circular Bosch Gks 150 220V",
                    Description = "La Sierra Circular Bosch 1500W 184mm es la herramienta perfecta para llevar tus proyectos de bricolaje al siguiente nivel. Con su potente motor de 1500W, esta sierra te brinda la potencia necesaria para cortar madera, plástico y otros materiales con facilidad y precisión.",
                    Price = 265000m,
                    Category = 3,
                    Discount = 5,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/300355-1200-1200?v=637665764833530000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Cubierta 175/65 R 14 82T Tl Maxisport",
                    Description = "Diseño simétrico, optimizado para un excelente fluir de agua y menor riesgo de acuaplanning sobre superficie mojada. Excelente dirigibilidad y desempeño superior en caminos secos y mojados. Construcción con bandas de alta flexibilidad para un gran confort. Alta resistencia a los Impactos. Bajo nivel sonoro proporcionando una conducción placentera.",
                    Price = 128000m,
                    Category = 4,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/307886-1200-1200?v=637671544058430000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Aceite 2 T Para Moto Bardahl 1Lt",
                    Description = "Aceite 2 T Para Moto Bardahl 1Lt de alto rendimiento, cuide y disfrute de su moto con la calidad del aceite Bardahl",
                    Price = 7600m,
                    Category = 4,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/280723-1200-1200?v=637651623825000000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Funda Cubre Asiento Sparco Rjo/Ngro 9Pc",
                    Description = "Fundas cubre asientos autos universal 9 piezas Sparco Este fabricado con polyester y cuenta con 2mm de espuma. Una tela suave y resistente que le brindará protección a tu asiento y será confortable para el uso diario.",
                    Price = 36370m,
                    Category = 4,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/341974-1200-1200?v=638107683258130000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Minicompresor Sincrolamp",
                    Description = "Minicompresor Sincrolamp 250 Psi 12 Volts con Accesorios para inflar",
                    Price = 16500m,
                    Category = 4,
                    Discount = 20,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/275875-1200-1200?v=637651592014930000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Interruptor Termomagnético 2x20A Sica",
                    Description = "Un interruptor termomagnético o llave térmica, es un dispositivo capaz de interrumpir la corriente eléctrica de un circuito cuando ésta sobrepasa ciertos valores máximos. Su funcionamiento se basa en dos de los efectos producidos por la circulación de corriente en un circuito: el magnético y el térmico.El dispositivo consta, por tanto, de dos partes, un electroimán y una lámina bimetálica, conectadas en serie y por las que circula la corriente que va hacia la carga. Al igual que los fusibles, los interruptores magnetotérmicos protegen la instalación contra sobrecargas y cortocircuito.",
                    Price = 7590m,
                    Category = 5,
                    Discount = 7,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/373534-1200-1200?v=638442978290670000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Sensor De Movimiento Exterior Hasta 500",
                    Description = "El sensor de movimiento exterior enciende la luminaria automáticamente al detectar presencia en la zona se puede programar el tiempo de encendido, regular la sensibilidad y anular la función de no encender de día.",
                    Price = 25520m,
                    Category = 5,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/336980-1200-1200?v=638016257102730000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Pila Energizer Max Aaa X4 Unidades Rojo 9,3X11X2 Unidadescm",
                    Description = "Energizer® es líder en la industria de proveer energía a la vida de las personas de forma responsable.\r\nHasta 10 años de vida útil.\r\n* Disponible en el mercado desde 1991 Uso Recomendado Ideal para equipos de uso continuo, controles remotos, radios, linternas, juguetes y reproductores de CD.",
                    Price = 4330m,
                    Category = 5,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/326041-1200-1200?v=637933357696170000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Cargador Pilas 4 Canales AA/AAA C/Corte Manual",
                    Description = "Cargador Pro1 USB para AA / AAA\r\nESPECIFICACIONES DEL PRODUCTO:\r\nPARA PILAS RECARGABLES Y BATERIAS\r\nTAMAÑOS AA - AAA\r\nMODELO DE CORTE MANUAL\r\nCARGA USB: INCLUYE CABLE.",
                    Price = 12290m,
                    Category = 5,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/353020-1200-1200?v=638195003302800000&width=1200&height=1200&aspect=true "
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Tanque de Agua Tricapa Gris 1100 L",
                    Description = "La estructura tricapa del tanque Rotoplas Gris garantiza la opacidad del interior del tanque, necesaria para evitar la fotosíntesis (desarrollo de algas y verdín) microorganismos y nasterias asociadas.",
                    Price = 232980m,
                    Category = 6,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/302366-1200-1200?v=637665767442600000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Bombas Presurizadoras 260 W",
                    Description = "La bomba Pluvius permite aumentar la presión de agua de una red hidráulica.\r\n\r\nEste producto cuenta con una entrada o tubo que aspira el agua y, posteriormente, este líquido es impulsado por un motor hacia el tubo de salida. Esto hace que el agua salga con mucha más presión y rapidez.\r\n\r\n",
                    Price = 164100m,
                    Category = 6,
                    Discount = 20,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/282119-1200-1200?v=637651632967100000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Pileta Duratop X De Patio",
                    Description = "Su unión deslizante asegura máxima estanqueidad en instalaciones empotradas, enterradas y a la vista, bajo techo y a la intemperie, en construcciones de todo tipo.\r\nMODELO: Pileta de patio M110.\r\nCONEXIÓN: Unión deslizante con guarnición monolabio.\r\nUSO RECOMENDADO: Desagüe cloacal y cloacal.",
                    Price = 8420m,
                    Category = 6,
                    Discount = 12,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/284828-1200-1200?v=637651648774300000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Biodigestor Para 7 Personas Septitank",
                    Description = "El Biodigestor SEPTITANK es un sistema de tratamiento primario de aguas residuales domésticas.\r\nIdeal para reemplazar eficientemente las fosas sépticas.\r\nMinimiza el impacto sobre el medio ambiente separando efluentes sólidos (bioabono) y líquidos (agua para riego).",
                    Price = 523385m,
                    Category = 6,
                    Discount = 8,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/281732-1200-1200?v=637651630950970000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Mesada De Cocina Durafort",
                    Description = "Mesada de Cocina Durafort Sin Zócalo 140x60 Cm Negro",
                    Price = 138240m,
                    Category = 7,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/333955-1200-1200?v=637992095004270000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Juego Cocina Monocomando Compacta 423/M4 Cromo",
                    Description = "La marca FV es un referente en el rubro de las griferías porque ofrece productos confortables y decorativos desde hace casi 100 años. El modelo 423/M4 es una solución estética y de gran calidad para tu hogar.",
                    Price = 73000m,
                    Category = 7,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/365635-1600-1600?v=638350437287400000&width=1600&height=1600&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Juego Monocomando Mesada Cocina Arizona Alto",
                    Description = "Este juego de monocomando para mesada de cocina Arizona Alto es la elección perfecta para aquellos que buscan combinar estilo y funcionalidad en su hogar.\r\n\r\nCon un diseño moderno y elegante, este juego de monocomando se convertirá en el centro de atención de tu cocina.",
                    Price = 91600m,
                    Category = 7,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/276856-1200-1200?v=637651598506800000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Alacena Sorrento De 140 Cm",
                    Description = "La cocina sorrento nos ofrece una línea de vanguardia, moderna, con tonos cálidos; su diseño modular resulta ideal para espacios reducidos sin sacrificarestilo y con la calidadque nos caracteriza.sistema de apertura con perfil de aluminio de doble agarre. ofrece tres configuraciones de medidas distintas que brindan versatilidad para responder a cualquier espacio",
                    Price = 134400m,
                    Category = 7,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/370610-1200-1200?v=638417995289500000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Placard 6 Puertas 4 Cajones Nordic 176X47X180 Cm Miel",
                    Description = "El Placard 6P 4CJ Nordic es la elección perfecta para aquellos que buscan una solución de almacenamiento elegante y funcional.\r\n\r\nCon sus dimensiones de 176x47x180 cm, este placard ofrece amplio espacio para guardar y organizar tu ropa y accesorios.",
                    Price = 294400m,
                    Category = 8,
                    Discount = 20,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/369976-1200-1200?v=638404070109600000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Cómoda Natural 3C 100X43X83 Cm R.Miel",
                    Description = "La cómoda natural 3C 100X43X83 R.Miel es una pieza de mobiliario que combina funcionalidad y estilo en un solo diseño. Con sus dimensiones de 100x43x83, esta cómoda ofrece amplio espacio de almacenamiento para tus pertenencias, manteniendo todo organizado y al alcance de la mano. Su acabado en miel le da un toque cálido y natural, perfecto para complementar cualquier estilo de decoración.",
                    Price = 113050m,
                    Category = 8,
                    Discount = 20,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/359052-1200-1200?v=638237262971500000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Colchon+Somier Resor Sab 90X190X50 Pmd",
                    Description = "colchon+somier resor sab 90x190x50 pmd Conjunto que incluye colchón y sommier Colchón de Resortes Bicónicos y marco perimetral de acero templado. Sommier de madera estacionada con patas de madera maciza a rosca. Tela Sábana en composé, detalles en tapa y lateral blanco, totalmente matelaseado. Medidas totales de juego armado 90x190x50 de alto (+/- 2 cm tolerancia)",
                    Price = 127597m,
                    Category = 8,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/338391-1200-1200?v=638031630115200000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Mesa De Luz Natural 40X40X60 Cm R.Miel",
                    Description = "Mejora tu Dormitorio con esta mesa de Luz con dos cajones con guias correderas metalicas en color Natural de diseño escandinavo con piezas en melamina texturada roble miel texturada, patas de madera maciza reforzadas de gran espesor que le da fortaleza y excelente laqueado mate que da muy buena terminación.",
                    Price = 62815m,
                    Category = 8,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/359059-1200-1200?v=638237264297870000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Unidad De Estado Sólido Samsung 990 Pro Pci-e 4.0 Nvme",
                    Description = "Considerada una de las marcas más innovadoras en el rubro de electrónica, Samsung ofrece productos de calidad y se destaca por su especialización en unidades de almacenamiento.\r\nEl 990 Pro MZ-V9P1T0B/AM está adaptado para que puedas acceder de forma rápida a tus documentos digitales gracias a su tecnología en estado sólido.",
                    Price = 219999m,
                    Category = 9,
                    Discount = 12,
                    ImageUrl = "https://http2.mlstatic.com/D_NQ_NP_716164-MLU74725749394_032024-O.webp"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Notebook Lenovo Legion 5 15ACH6A",
                    Description = "Notebook Gamer Lenovo Legion 5 15ACH6A WQHD 2K 15.6\" R5 5600H 16GB (2x8GB) 512GB SSD NVME RX6600M 8GB W11 165Hz Silver",
                    Price = 1153600m,
                    Category = 9,
                    Discount = 10,
                    ImageUrl = "https://imagenes.compragamer.com/productos/compragamer_Imganen_general_38593_Notebook_Gamer_Lenovo_Legion_5_15ACH6A_WQHD_2K_15.6__R5_5600H_16GB__2x8GB__512GB_SSD_NVME_RX6600M_8GB_W11_165Hz_Silver_107e6074-grn.jpg"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Placa de Video XFX Radeon RX 580 8GB GDDR5",
                    Description = "Velocidad Del Core Base\r\n1366 mhz\r\nVelocidad De Memoria\r\n8000 mhz\r\nTipo De Memoria\r\nGDDR5\r\nCapacidad De Memoria\r\n8 gb\r\nInterface De Memoria\r\n256 bits\r\nVelocidad Del Core Turbo\r\n1386 mhz\r\nTipos De Procesos\r\nStream\r\nCantidad De Procesos\r\n2048",
                    Price = 185400m,
                    Category = 9,
                    Discount = 20,
                    ImageUrl = "https://imagenes.compragamer.com/productos/compragamer_Imganen_general_39191_Placa_de_Video_XFX_Radeon_RX_580_8GB_GDDR5_GTS_XXX_8c7b9091-grn.jpg"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Samsung Galaxy A14 128gb 4gb Ram Negro",
                    Description = "El Samsung Galaxy A14 de 128GB y 4GB de RAM en color negro es el celular perfecto para aquellos que buscan un dispositivo potente y elegante. Con su pantalla Full HD+ de 6.6 pulgadas, disfrutarás de imágenes nítidas y colores vibrantes en todo momento. Gracias a su sistema operativo Android, tendrás acceso a miles de aplicaciones y juegos para sacar el máximo provecho a tu smartphone. Además, su capacidad de almacenamiento de 128GB te permitirá guardar todas tus fotos, videos y aplicaciones sin preocuparte por el espacio. La memoria RAM de 4GB garantiza un rendimiento fluido y rápido, incluso al utilizar varias aplicaciones al mismo tiempo. Con su batería de 5000 mAh, podrás disfrutar de largas horas de uso sin preocuparte por cargarlo constantemente. Y aunque no es Dual SIM, su conectividad 4G/LTE te asegura una navegación rápida y estable en todo momento. No esperes más y lleva contigo el Samsung Galaxy A14, el celular que combina estilo, potencia y funcionalidad en un solo dispositivo.",
                    Price = 239999m,
                    Category = 9,
                    Discount = 25,
                    ImageUrl = "https://http2.mlstatic.com/D_NQ_NP_823777-MLU54974953656_042023-O.webp"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Sombrilla De Aluminio 3.5 Mts",
                    Description = "Sus medidas, como muestran las imágenes son 2,45mts de Altura x 3 mts de Diámetro de cobertura en forma octogonal para permitir mas área de Sombra total. ATENCIÓN: El artículo viene SIN LA BASE. Características: Color: Crudo, Material Aluminio 19%, Acero 22%, Poliéster 48%, Plástico 11%. Peso Neto 5,1 kg. Con Inclinación y Manivela para Apertura. Diámetro del caño de la base: 38mm. Medidas caja: 155x13,7x13,7",
                    Price = 135996m,
                    Category = 10,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/370037-1200-1200?v=638404197434000000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Silla de playa Outzen New Asis negro 110x65x165 cm",
                    Description = "Con su diseño ergonómico y su construcción duradera, la reposera Outzen Asis te brinda la combinación perfecta de estilo y funcionalidad. Ya sea en la playa, en el jardín o en cualquier otro lugar al aire libre, esta reposera te invita a relajarte y disfrutar del momento con total comodidad.\r\n\r\n",
                    Price = 43464m,
                    Category = 10,
                    Discount = 10,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/314562-1200-1200?v=637804601200000000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Parrilla Chulengo a Leña Tromen",
                    Description = "Accesible como ninguno, diseño único y funcional. Disfruta del sabor aumado de un clásico a menor precio con la mejor calidad.",
                    Price = 308792m,
                    Category = 10,
                    Discount = 5,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/368266-1200-1200?v=638386939442200000&width=1200&height=1200&aspect=true"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Pileta Lona Pelopincho 1055 4.500L 75X200X300 Cm 26,4Kg",
                    Description = "Prepárate para disfrutar del verano con la Pileta de Lona Pelopincho 1055, con una generosa capacidad de 4.500 litros y dimensiones de 75x200x300 cm. Esta espaciosa pileta ofrece diversión para toda la familia en la comodidad de tu propio patio trasero.",
                    Price = 178991m,
                    Category = 10,
                    Discount = 15,
                    ImageUrl = "https://arcencohogar.vtexassets.com/arquivos/ids/307538-1200-1200?v=637665774493470000&width=1200&height=1200&aspect=true"
                }
                );
        }
        
    }
}
