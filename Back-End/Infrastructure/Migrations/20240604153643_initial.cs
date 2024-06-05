using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPay = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_Category",
                        column: x => x.Category,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_Product",
                        column: x => x.Product,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_Sale",
                        column: x => x.Sale,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Electrodomesticos" },
                    { 2, "Baño" },
                    { 3, "Herramientas" },
                    { 4, "Automotor" },
                    { 5, "Electricidad" },
                    { 6, "Plomeria" },
                    { 7, "Cocina" },
                    { 8, "Muebleria" },
                    { 9, "Tecnologia" },
                    { 10, "Jardin" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "Discount", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("08a1dae2-b705-4386-b1d8-30aa129a1306"), 6, "La bomba Pluvius permite aumentar la presión de agua de una red hidráulica.\r\n\r\nEste producto cuenta con una entrada o tubo que aspira el agua y, posteriormente, este líquido es impulsado por un motor hacia el tubo de salida. Esto hace que el agua salga con mucha más presión y rapidez.\r\n\r\n", 20, "https://arcencohogar.vtexassets.com/arquivos/ids/282119-1200-1200?v=637651632967100000&width=1200&height=1200&aspect=true", "Bombas Presurizadoras 260 W", 164100m },
                    { new Guid("113bfa1f-4b06-4e03-83e3-a2d36b3d2db2"), 2, "El sanitario 2 piezas Hervas Blanco es la elección perfecta para tu baño. Con su diseño elegante y moderno, este sanitario no solo cumple con su función principal, sino que también agrega un toque de estilo a tu espacio.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/325325-1200-1200?v=637928028976170000&width=1200&height=1200&aspect=true", "Inodoro Two Piece con Depósito y Asiento", 141700m },
                    { new Guid("1818daae-b1ad-4abc-8abd-726bffb20ea0"), 10, "Sus medidas, como muestran las imágenes son 2,45mts de Altura x 3 mts de Diámetro de cobertura en forma octogonal para permitir mas área de Sombra total. ATENCIÓN: El artículo viene SIN LA BASE. Características: Color: Crudo, Material Aluminio 19%, Acero 22%, Poliéster 48%, Plástico 11%. Peso Neto 5,1 kg. Con Inclinación y Manivela para Apertura. Diámetro del caño de la base: 38mm. Medidas caja: 155x13,7x13,7", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/370037-1200-1200?v=638404197434000000&width=1200&height=1200&aspect=true", "Sombrilla De Aluminio 3.5 Mts", 135996m },
                    { new Guid("198730db-c341-41cd-89fb-de467348d3f9"), 4, "Minicompresor Sincrolamp 250 Psi 12 Volts con Accesorios para inflar", 20, "https://arcencohogar.vtexassets.com/arquivos/ids/275875-1200-1200?v=637651592014930000&width=1200&height=1200&aspect=true", "Minicompresor Sincrolamp", 16500m },
                    { new Guid("1d9a84cf-695e-4e06-aab8-89c81aa299da"), 5, "El sensor de movimiento exterior enciende la luminaria automáticamente al detectar presencia en la zona se puede programar el tiempo de encendido, regular la sensibilidad y anular la función de no encender de día.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/336980-1200-1200?v=638016257102730000&width=1200&height=1200&aspect=true", "Sensor De Movimiento Exterior Hasta 500", 25520m },
                    { new Guid("20995732-e34b-4969-9954-2c58ad0f4356"), 5, "Cargador Pro1 USB para AA / AAA\r\nESPECIFICACIONES DEL PRODUCTO:\r\nPARA PILAS RECARGABLES Y BATERIAS\r\nTAMAÑOS AA - AAA\r\nMODELO DE CORTE MANUAL\r\nCARGA USB: INCLUYE CABLE.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/353020-1200-1200?v=638195003302800000&width=1200&height=1200&aspect=true ", "Cargador Pilas 4 Canales AA/AAA C/Corte Manual", 12290m },
                    { new Guid("219dda57-2a6f-476a-9357-66c164f58d05"), 1, "Climatizar tus espacios a lo largo del año es sin duda algo importante para tu comodidad y la de tus seres queridos. Contar con un aire acondicionado con función frío/calor es la mejor decisión. Con este aire Nex conseguí una mejor relación costo-beneficio.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/365635-1200-1200?v=638350437287400000&width=1200&height=1200&aspect=true", "Aire Acondicionado Nex On Off 2752fn", 850000m },
                    { new Guid("25f7bb63-c1bd-47df-a636-9e6138d4f891"), 10, "Con su diseño ergonómico y su construcción duradera, la reposera Outzen Asis te brinda la combinación perfecta de estilo y funcionalidad. Ya sea en la playa, en el jardín o en cualquier otro lugar al aire libre, esta reposera te invita a relajarte y disfrutar del momento con total comodidad.\r\n\r\n", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/314562-1200-1200?v=637804601200000000&width=1200&height=1200&aspect=true", "Silla de playa Outzen New Asis negro 110x65x165 cm", 43464m },
                    { new Guid("270c0748-534c-480f-ac14-1d09a6295d15"), 1, "Marca lider del mercado - 6 KG - 800 RPM - Eficiencia energetica A+. Color blanco.", 5, "https://arcencohogar.vtexassets.com/arquivos/ids/372022-1200-1200?v=638428189685100000&width=1200&height=1200&aspect=true", "Lavarropas Drean Eco Next 6Kg Blanco", 54996m },
                    { new Guid("2ad6c19b-5cce-404f-9c98-bfa53d8220f7"), 7, "La marca FV es un referente en el rubro de las griferías porque ofrece productos confortables y decorativos desde hace casi 100 años. El modelo 423/M4 es una solución estética y de gran calidad para tu hogar.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/365635-1600-1600?v=638350437287400000&width=1600&height=1600&aspect=true", "Juego Cocina Monocomando Compacta 423/M4 Cromo", 73000m },
                    { new Guid("2d2d7f0f-0583-4d32-a294-4f4be6da9d48"), 4, "Diseño simétrico, optimizado para un excelente fluir de agua y menor riesgo de acuaplanning sobre superficie mojada. Excelente dirigibilidad y desempeño superior en caminos secos y mojados. Construcción con bandas de alta flexibilidad para un gran confort. Alta resistencia a los Impactos. Bajo nivel sonoro proporcionando una conducción placentera.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/307886-1200-1200?v=637671544058430000&width=1200&height=1200&aspect=true", "Cubierta 175/65 R 14 82T Tl Maxisport", 128000m },
                    { new Guid("2e172cfb-105d-4ffc-b924-c1997d2d824e"), 6, "La estructura tricapa del tanque Rotoplas Gris garantiza la opacidad del interior del tanque, necesaria para evitar la fotosíntesis (desarrollo de algas y verdín) microorganismos y nasterias asociadas.", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/302366-1200-1200?v=637665767442600000&width=1200&height=1200&aspect=true", "Tanque de Agua Tricapa Gris 1100 L", 232980m },
                    { new Guid("2e6897ea-e576-4033-b274-78bbb8fdc26a"), 2, "set acc. de baño 5 piezas marayui imp", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/340426-1200-1200?v=638085454583770000&width=1200&height=1200&aspect=true", "Set de accesorios de baño Vessanti Marayui 5 piezas", 29000m },
                    { new Guid("2f9b3557-6c10-4985-b282-e84d9243a78e"), 9, "Velocidad Del Core Base\r\n1366 mhz\r\nVelocidad De Memoria\r\n8000 mhz\r\nTipo De Memoria\r\nGDDR5\r\nCapacidad De Memoria\r\n8 gb\r\nInterface De Memoria\r\n256 bits\r\nVelocidad Del Core Turbo\r\n1386 mhz\r\nTipos De Procesos\r\nStream\r\nCantidad De Procesos\r\n2048", 20, "https://imagenes.compragamer.com/productos/compragamer_Imganen_general_39191_Placa_de_Video_XFX_Radeon_RX_580_8GB_GDDR5_GTS_XXX_8c7b9091-grn.jpg", "Placa de Video XFX Radeon RX 580 8GB GDDR5", 185400m },
                    { new Guid("3948c5e1-91e7-448c-832a-78728657576b"), 8, "Mejora tu Dormitorio con esta mesa de Luz con dos cajones con guias correderas metalicas en color Natural de diseño escandinavo con piezas en melamina texturada roble miel texturada, patas de madera maciza reforzadas de gran espesor que le da fortaleza y excelente laqueado mate que da muy buena terminación.", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/359059-1200-1200?v=638237264297870000&width=1200&height=1200&aspect=true", "Mesa De Luz Natural 40X40X60 Cm R.Miel", 62815m },
                    { new Guid("4a66a55c-7e35-4fa5-97fa-7a0f33a2ed5a"), 10, "Prepárate para disfrutar del verano con la Pileta de Lona Pelopincho 1055, con una generosa capacidad de 4.500 litros y dimensiones de 75x200x300 cm. Esta espaciosa pileta ofrece diversión para toda la familia en la comodidad de tu propio patio trasero.", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/307538-1200-1200?v=637665774493470000&width=1200&height=1200&aspect=true", "Pileta Lona Pelopincho 1055 4.500L 75X200X300 Cm 26,4Kg", 178991m },
                    { new Guid("55bbc386-5767-42cb-9c0b-cb26e8063786"), 7, "Este juego de monocomando para mesada de cocina Arizona Alto es la elección perfecta para aquellos que buscan combinar estilo y funcionalidad en su hogar.\r\n\r\nCon un diseño moderno y elegante, este juego de monocomando se convertirá en el centro de atención de tu cocina.", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/276856-1200-1200?v=637651598506800000&width=1200&height=1200&aspect=true", "Juego Monocomando Mesada Cocina Arizona Alto", 91600m },
                    { new Guid("5de687fe-5005-4e47-bf9c-07bdfc20f798"), 6, "Su unión deslizante asegura máxima estanqueidad en instalaciones empotradas, enterradas y a la vista, bajo techo y a la intemperie, en construcciones de todo tipo.\r\nMODELO: Pileta de patio M110.\r\nCONEXIÓN: Unión deslizante con guarnición monolabio.\r\nUSO RECOMENDADO: Desagüe cloacal y cloacal.", 12, "https://arcencohogar.vtexassets.com/arquivos/ids/284828-1200-1200?v=637651648774300000&width=1200&height=1200&aspect=true", "Pileta Duratop X De Patio", 8420m },
                    { new Guid("5e17c86d-a7ac-433c-b66f-e2d231f2cf3b"), 8, "El Placard 6P 4CJ Nordic es la elección perfecta para aquellos que buscan una solución de almacenamiento elegante y funcional.\r\n\r\nCon sus dimensiones de 176x47x180 cm, este placard ofrece amplio espacio para guardar y organizar tu ropa y accesorios.", 20, "https://arcencohogar.vtexassets.com/arquivos/ids/369976-1200-1200?v=638404070109600000&width=1200&height=1200&aspect=true", "Placard 6 Puertas 4 Cajones Nordic 176X47X180 Cm Miel", 294400m },
                    { new Guid("7a80400a-e4cd-439f-a672-5f86b8892a7b"), 1, "El ventilador PIONEER es mas econmico ya que ahorra un 50% de energia electrica\r\nEs practico, facil de trasladar y adaptabilidad.\r\nSus aspas Metalicas lo hacen un producto de gran durabilidad.\r\nsu base de apoyo firme y su rejilla protectora metalica lo hacen un producto seguro", 12, "https://arcencohogar.vtexassets.com/arquivos/ids/371560-1200-1200?v=638422437614700000&width=1200&height=1200&aspect=true", "Ventilador Industrial 18 Metal Pioneer", 54996m },
                    { new Guid("836d5c34-c8e2-4dbb-a109-f8dc53ee5874"), 4, "Fundas cubre asientos autos universal 9 piezas Sparco Este fabricado con polyester y cuenta con 2mm de espuma. Una tela suave y resistente que le brindará protección a tu asiento y será confortable para el uso diario.", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/341974-1200-1200?v=638107683258130000&width=1200&height=1200&aspect=true", "Funda Cubre Asiento Sparco Rjo/Ngro 9Pc", 36370m },
                    { new Guid("90f0e7e0-aa7a-4e9b-9c69-a4295f28cb2d"), 1, "Esta heladera cuenta con vidrio templado en todos los estantes, ofreciendo calidad y resistencia. Los estantes son flexibles con altura regulable y cómodos anaqueles desmontables en la puerta.Los estantes se pueden mover en altura para optimizar el espacio, en función de las necesidades de guardado.", 16, "https://arcencohogar.vtexassets.com/arquivos/ids/369370-1200-1200?v=638392835522730000&width=1200&height=1200&aspect=true", "Heladera Eslabon de Lujo Erd29Ab 273 L", 530000m },
                    { new Guid("9791d616-5db1-4a59-b268-528bf38e92d3"), 10, "Accesible como ninguno, diseño único y funcional. Disfruta del sabor aumado de un clásico a menor precio con la mejor calidad.", 5, "https://arcencohogar.vtexassets.com/arquivos/ids/368266-1200-1200?v=638386939442200000&width=1200&height=1200&aspect=true", "Parrilla Chulengo a Leña Tromen", 308792m },
                    { new Guid("98de0248-e6ca-4d4b-aca4-3b2aa5ae2d26"), 7, "La cocina sorrento nos ofrece una línea de vanguardia, moderna, con tonos cálidos; su diseño modular resulta ideal para espacios reducidos sin sacrificarestilo y con la calidadque nos caracteriza.sistema de apertura con perfil de aluminio de doble agarre. ofrece tres configuraciones de medidas distintas que brindan versatilidad para responder a cualquier espacio", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/370610-1200-1200?v=638417995289500000&width=1200&height=1200&aspect=true", "Alacena Sorrento De 140 Cm", 134400m },
                    { new Guid("9b9c5bec-1a8a-4a54-9f68-3c82c30a2064"), 5, "Un interruptor termomagnético o llave térmica, es un dispositivo capaz de interrumpir la corriente eléctrica de un circuito cuando ésta sobrepasa ciertos valores máximos. Su funcionamiento se basa en dos de los efectos producidos por la circulación de corriente en un circuito: el magnético y el térmico.El dispositivo consta, por tanto, de dos partes, un electroimán y una lámina bimetálica, conectadas en serie y por las que circula la corriente que va hacia la carga. Al igual que los fusibles, los interruptores magnetotérmicos protegen la instalación contra sobrecargas y cortocircuito.", 7, "https://arcencohogar.vtexassets.com/arquivos/ids/373534-1200-1200?v=638442978290670000&width=1200&height=1200&aspect=true", "Interruptor Termomagnético 2x20A Sica", 7590m },
                    { new Guid("9de70006-4869-479c-ae74-1fe3d9fff50a"), 7, "Mesada de Cocina Durafort Sin Zócalo 140x60 Cm Negro", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/333955-1200-1200?v=637992095004270000&width=1200&height=1200&aspect=true", "Mesada De Cocina Durafort", 138240m },
                    { new Guid("b8a0aa0c-ffd9-49e5-93bb-eed52befec6a"), 3, "La Sierra Circular Bosch 1500W 184mm es la herramienta perfecta para llevar tus proyectos de bricolaje al siguiente nivel. Con su potente motor de 1500W, esta sierra te brinda la potencia necesaria para cortar madera, plástico y otros materiales con facilidad y precisión.", 5, "https://arcencohogar.vtexassets.com/arquivos/ids/300355-1200-1200?v=637665764833530000&width=1200&height=1200&aspect=true", "Sierra Circular Bosch Gks 150 220V", 265000m },
                    { new Guid("c157c1e4-86b9-4081-a62b-313923580c19"), 6, "El Biodigestor SEPTITANK es un sistema de tratamiento primario de aguas residuales domésticas.\r\nIdeal para reemplazar eficientemente las fosas sépticas.\r\nMinimiza el impacto sobre el medio ambiente separando efluentes sólidos (bioabono) y líquidos (agua para riego).", 8, "https://arcencohogar.vtexassets.com/arquivos/ids/281732-1200-1200?v=637651630950970000&width=1200&height=1200&aspect=true", "Biodigestor Para 7 Personas Septitank", 523385m },
                    { new Guid("c838915d-b910-4a3a-a0f9-e9170bb39271"), 3, "La amoladora Bosch GWS 700 es la herramienta perfecta para aquellos que buscan potencia y precisión en sus proyectos de bricolaje. Con su motor de 220V, esta amoladora es capaz de realizar cortes y pulidos de alta calidad en una amplia variedad de materiales. Además, incluye 5 discos adicionales, lo que te permite comenzar a trabajar de inmediato sin tener que preocuparte por comprar accesorios adicionales.", 15, "https://arcencohogar.vtexassets.com/arquivos/ids/367406-1200-1200?v=638357448136970000&width=1200&height=1200&aspect=true", "Amoladora Bosch Gws 700 + 5 Discos 220V", 126000m },
                    { new Guid("cb3de379-622a-4d80-9e22-588aaeb8edc8"), 4, "Aceite 2 T Para Moto Bardahl 1Lt de alto rendimiento, cuide y disfrute de su moto con la calidad del aceite Bardahl", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/280723-1200-1200?v=637651623825000000&width=1200&height=1200&aspect=true", "Aceite 2 T Para Moto Bardahl 1Lt", 7600m },
                    { new Guid("d8185dfe-13c7-49a2-86b9-a37a4efb3869"), 2, "Bienvenido al mundo de la elegancia y la funcionalidad para tu baño. Nuestros accesorios están diseñados pensando en la excelencia en cada detalle. Con un enfoque meticuloso en la calidad y la durabilidad, estos accesorios están destinados a transformar tu espacio de baño en un oasis de comodidad y estilo.", 12, "https://arcencohogar.vtexassets.com/arquivos/ids/375419-1200-1200?v=638478528895830000&width=1200&height=1200&aspect=true", "Jabonera Cromo Andez Klub", 17000m },
                    { new Guid("d841c999-e200-4c31-9669-fdcaf4f0f0ee"), 8, "colchon+somier resor sab 90x190x50 pmd Conjunto que incluye colchón y sommier Colchón de Resortes Bicónicos y marco perimetral de acero templado. Sommier de madera estacionada con patas de madera maciza a rosca. Tela Sábana en composé, detalles en tapa y lateral blanco, totalmente matelaseado. Medidas totales de juego armado 90x190x50 de alto (+/- 2 cm tolerancia)", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/338391-1200-1200?v=638031630115200000&width=1200&height=1200&aspect=true", "Colchon+Somier Resor Sab 90X190X50 Pmd", 127597m },
                    { new Guid("d9224164-84cd-4f4c-8326-c874ad4dd36d"), 9, "Considerada una de las marcas más innovadoras en el rubro de electrónica, Samsung ofrece productos de calidad y se destaca por su especialización en unidades de almacenamiento.\r\nEl 990 Pro MZ-V9P1T0B/AM está adaptado para que puedas acceder de forma rápida a tus documentos digitales gracias a su tecnología en estado sólido.", 12, "https://http2.mlstatic.com/D_NQ_NP_716164-MLU74725749394_032024-O.webp", "Unidad De Estado Sólido Samsung 990 Pro Pci-e 4.0 Nvme", 219999m },
                    { new Guid("e1a69a0e-c82b-41b4-8c9c-67bd25795a1d"), 9, "Notebook Gamer Lenovo Legion 5 15ACH6A WQHD 2K 15.6\" R5 5600H 16GB (2x8GB) 512GB SSD NVME RX6600M 8GB W11 165Hz Silver", 10, "https://imagenes.compragamer.com/productos/compragamer_Imganen_general_38593_Notebook_Gamer_Lenovo_Legion_5_15ACH6A_WQHD_2K_15.6__R5_5600H_16GB__2x8GB__512GB_SSD_NVME_RX6600M_8GB_W11_165Hz_Silver_107e6074-grn.jpg", "Notebook Lenovo Legion 5 15ACH6A", 1153600m },
                    { new Guid("e69966fb-7d1f-4cb2-88cc-259aa811a88d"), 3, "El Taladro Perc Brushless 18V + 2 Bat de Easy es la herramienta perfecta para llevar a cabo tus proyectos de bricolaje con facilidad y eficiencia.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/324566-1200-1200?v=637915901462570000&width=1200&height=1200&aspect=true", "Taladro Atornillador Percutor Bosch Gsb 18V-50", 842000m },
                    { new Guid("efda0b4b-2ff9-4df3-a189-9e66ece49f16"), 9, "El Samsung Galaxy A14 de 128GB y 4GB de RAM en color negro es el celular perfecto para aquellos que buscan un dispositivo potente y elegante. Con su pantalla Full HD+ de 6.6 pulgadas, disfrutarás de imágenes nítidas y colores vibrantes en todo momento. Gracias a su sistema operativo Android, tendrás acceso a miles de aplicaciones y juegos para sacar el máximo provecho a tu smartphone. Además, su capacidad de almacenamiento de 128GB te permitirá guardar todas tus fotos, videos y aplicaciones sin preocuparte por el espacio. La memoria RAM de 4GB garantiza un rendimiento fluido y rápido, incluso al utilizar varias aplicaciones al mismo tiempo. Con su batería de 5000 mAh, podrás disfrutar de largas horas de uso sin preocuparte por cargarlo constantemente. Y aunque no es Dual SIM, su conectividad 4G/LTE te asegura una navegación rápida y estable en todo momento. No esperes más y lleva contigo el Samsung Galaxy A14, el celular que combina estilo, potencia y funcionalidad en un solo dispositivo.", 25, "https://http2.mlstatic.com/D_NQ_NP_823777-MLU54974953656_042023-O.webp", "Samsung Galaxy A14 128gb 4gb Ram Negro", 239999m },
                    { new Guid("f5c2815d-8346-4efd-985a-9eab62f84524"), 8, "La cómoda natural 3C 100X43X83 R.Miel es una pieza de mobiliario que combina funcionalidad y estilo en un solo diseño. Con sus dimensiones de 100x43x83, esta cómoda ofrece amplio espacio de almacenamiento para tus pertenencias, manteniendo todo organizado y al alcance de la mano. Su acabado en miel le da un toque cálido y natural, perfecto para complementar cualquier estilo de decoración.", 20, "https://arcencohogar.vtexassets.com/arquivos/ids/359052-1200-1200?v=638237262971500000&width=1200&height=1200&aspect=true", "Cómoda Natural 3C 100X43X83 Cm R.Miel", 113050m },
                    { new Guid("f67da113-44f7-4788-84f7-6977659b6d06"), 5, "Energizer® es líder en la industria de proveer energía a la vida de las personas de forma responsable.\r\nHasta 10 años de vida útil.\r\n* Disponible en el mercado desde 1991 Uso Recomendado Ideal para equipos de uso continuo, controles remotos, radios, linternas, juguetes y reproductores de CD.", 10, "https://arcencohogar.vtexassets.com/arquivos/ids/326041-1200-1200?v=637933357696170000&width=1200&height=1200&aspect=true", "Pila Energizer Max Aaa X4 Unidades Rojo 9,3X11X2 Unidadescm", 4330m },
                    { new Guid("fc7872d3-35c0-4e40-b190-65c9334f45e8"), 2, "Este juego de baño con tapa de ciprés 103/N2 CR es la elección perfecta para darle un toque de elegancia y estilo a tu baño.", 25, "https://arcencohogar.vtexassets.com/arquivos/ids/356354-1200-1200?v=638223661536300000&width=1200&height=1200&aspect=true", "Juego De Ducha Embutida Con Transferencia Cipres 103 - N2 Cromo", 58996m },
                    { new Guid("fd642e4b-a907-4cfc-87c7-6cdf52c28855"), 3, "La Hormigonera Deper 150 LTS es la compañera perfecta para tus proyectos de construcción.\r\n\r\nCon su capacidad de 150 litros, podrás mezclar grandes cantidades de hormigón de manera eficiente y rápida.", 20, "https://arcencohogar.vtexassets.com/arquivos/ids/273755-1200-1200?v=637651577792600000&width=1200&height=1200&aspect=true", "Hormigonera Compacta 130 L", 464550m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category",
                table: "Product",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Product",
                table: "SaleProduct",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Sale",
                table: "SaleProduct",
                column: "Sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
