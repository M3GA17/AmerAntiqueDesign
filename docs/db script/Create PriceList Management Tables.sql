-- Creazione della tabella priceLists_tb
CREATE TABLE IF NOT EXISTS public."priceLists_tb" (
    "idPriceList" UUID NOT NULL,
    "name" VARCHAR(128) NOT NULL,
    "description" VARCHAR(512),
    "startDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "endDate" TIMESTAMP WITH TIME ZONE,
    "isEnabled" BOOLEAN NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_priceLists_tb_idPriceList" PRIMARY KEY ("idPriceList")
);

ALTER TABLE public."priceLists_tb"
ADD CONSTRAINT "FK_priceLists_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."priceLists_tb"
ADD CONSTRAINT "FK_priceLists_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

-- Creazione della tabella priceListItems_tb
CREATE TABLE IF NOT EXISTS public."priceListItems_tb" (
    "idPriceListItem" UUID NOT NULL,
    "idPriceList" UUID NOT NULL,
    "idProduct" UUID NOT NULL,
    "price" NUMERIC(18,2) NOT NULL,
    "previousPrice" NUMERIC(18,2),
    "startDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "endDate" TIMESTAMP WITH TIME ZONE,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_priceListItems_tb_idPriceListItem" PRIMARY KEY ("idPriceListItem")
);

ALTER TABLE public."priceListItems_tb"
ADD CONSTRAINT "FK_price_list_items_tb_TO_priceLists_tb_idPriceList" FOREIGN KEY ("idPriceList")
REFERENCES public."priceLists_tb" ("idPriceList") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."priceListItems_tb"
ADD CONSTRAINT "FK_price_list_items_tb_TO_products_tb_idProduct" FOREIGN KEY ("idProduct")
REFERENCES public."products_tb" ("idProduct") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."priceListItems_tb"
ADD CONSTRAINT "FK_price_list_items_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."priceListItems_tb"
ADD CONSTRAINT "FK_price_list_items_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;