-- Creazione della tabella warehouses_tb
CREATE TABLE IF NOT EXISTS public."warehouses_tb" (
    "idWarehouse" UUID NOT NULL,
    "name" VARCHAR(128) NOT NULL,
    "address" VARCHAR(512),
    "isVirtual" BOOLEAN NOT NULL,
    "isEnabled" BOOLEAN NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_warehouses_tb_idWarehouse" PRIMARY KEY ("idWarehouse")
);

ALTER TABLE public."warehouses_tb"
ADD CONSTRAINT "FK_warehouses_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."warehouses_tb"
ADD CONSTRAINT "FK_warehouses_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;


-- Creazione della tabella warehouseProducts_tb
CREATE TABLE IF NOT EXISTS public."warehouseProducts_tb" (
    "idWarehouseProduct" UUID NOT NULL,
    "idWarehouse" UUID NOT NULL,
    "idProduct" UUID NOT NULL,
    "quantity" INTEGER NOT NULL, -- lo metto giusto per completezza ma è sempre 1
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_warehouseProducts_tb_idWarehouseProduct" PRIMARY KEY ("idWarehouseProduct")
);

ALTER TABLE public."warehouseProducts_tb"
ADD CONSTRAINT "FK_warehouseProducts_tb_TO_warehouses_tb_idWarehouse" FOREIGN KEY ("idWarehouse")
REFERENCES public."warehouses_tb" ("idWarehouse") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."warehouseProducts_tb"
ADD CONSTRAINT "FK_warehouseProducts_tb_TO_products_tb_idProduct" FOREIGN KEY ("idProduct")
REFERENCES public."products_tb" ("idProduct") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."warehouseProducts_tb"
ADD CONSTRAINT "FK_warehouseProducts_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."warehouseProducts_tb"
ADD CONSTRAINT "FK_warehouseProducts_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;
