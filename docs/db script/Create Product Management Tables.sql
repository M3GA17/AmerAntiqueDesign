CREATE TABLE IF NOT EXISTS public."categories_tb" (
    "idCategory" UUID NOT NULL,
    "name" VARCHAR(128) NOT NULL,
    "description" VARCHAR(512),
    "idCategoryParent" UUID,
    "isEnabled" BOOLEAN NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_categories_tb_idCategory" PRIMARY KEY ("idCategory")
);

ALTER TABLE public."categories_tb"
ADD CONSTRAINT "FK_categories_tb_TO_categories_tb_idCategoryParent" FOREIGN KEY ("idCategoryParent")
REFERENCES public."categories_tb" ("idCategory") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."categories_tb"
ADD CONSTRAINT "FK_categories_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."categories_tb"
ADD CONSTRAINT "FK_categories_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

-- Creazione della tabella products_tb
CREATE TABLE IF NOT EXISTS public."products_tb" (
    "idProduct" UUID NOT NULL,
    "serialNumber" VARCHAR(7) NOT NULL UNIQUE,
    "name" VARCHAR(512) NOT NULL,
    "description" VARCHAR(2048),
    "idCategory" UUID NOT NULL,
    "idProductStatus" CHAR(5) NOT NULL,
    "height" INTEGER NOT NULL,
    "width" INTEGER NOT NULL,
    "depth" INTEGER NOT NULL,
    "isBulky" BOOLEAN NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
	"databaseVersion" INTEGER NOT NULL,
    CONSTRAINT "PK_products_tb_idProduct" PRIMARY KEY ("idProduct"),
    CONSTRAINT "UI_products_tb_serialNumber" UNIQUE ("serialNumber")
);

ALTER TABLE public."products_tb"
ADD CONSTRAINT "FK_products_tb_TO_categories_tb_idCategory" FOREIGN KEY ("idCategory")
REFERENCES public."categories_tb" ("idCategory") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."products_tb"
ADD CONSTRAINT "FK_products_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."products_tb"
ADD CONSTRAINT "FK_products_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."products_tb"
ADD CONSTRAINT "FK_products_tb_TO_productStatuses_tb_idProductStatus" FOREIGN KEY ("idProductStatus")
REFERENCES public."productStatuses_tb" ("idProductStatus") ON UPDATE NO ACTION ON DELETE RESTRICT;


-- Creazione della tabella productPhotos_tb
CREATE TABLE IF NOT EXISTS public."productPhotos_tb" (
    "idProductPhoto" UUID NOT NULL,
    "idProduct" UUID NOT NULL,
    "name" VARCHAR(512) NOT NULL,
    "url" VARCHAR(2048) NOT NULL, -- or path
    "isMain" BOOLEAN NOT NULL,
    "displayOrder" INTEGER NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_productPhotos_tb_idProductPhoto" PRIMARY KEY ("idProductPhoto")
);

ALTER TABLE public."productPhotos_tb"
ADD CONSTRAINT "FK_productPhotos_tb_TO_products_tb_idProduct" FOREIGN KEY ("idProduct")
REFERENCES public."products_tb" ("idProduct") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."productPhotos_tb"
ADD CONSTRAINT "FK_productPhotos_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."productPhotos_tb"
ADD CONSTRAINT "FK_productPhotos_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

CREATE TABLE IF NOT EXISTS public."productStatuses_tb" (
    "idProductStatus" CHAR(5) NOT NULL,
    "statusName" VARCHAR(128),
    CONSTRAINT "PK_productStatuses_tb_idProductStatus" PRIMARY KEY ("idProductStatus")
);

INSERT INTO "public"."productStatuses_tb"("idProductStatus","statusName") VALUES
('PsNll' ,'Empty'),
('PsDrf' ,'Draft'),
('PsRpr' ,'Repair'),
('PsIns' ,'Inserted'),
('PsAvl' ,'Available'),
('PsUnv' ,'Unavailable'),
('PsSld' ,'Sold'),
('PsArc' ,'Archived'); 