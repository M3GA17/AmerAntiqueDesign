-- Creazione della tabella salesOrders_tb
CREATE TABLE IF NOT EXISTS public."salesOrders_tb" (
    "idSalesOrder" UUID NOT NULL,
    "orderDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "totalAmount" NUMERIC(18,2) NOT NULL,
    "idSalesOrderStatus" CHAR(5) NOT NULL,
    "idSalesOrderMode" CHAR(5) NOT NULL,
    "idPaymentMethod" CHAR(5) NOT NULL,
    "idInvoice" UUID,
    "customerFirstName" VARCHAR(128),
    "customerLastName" VARCHAR(128),
    "customerAddress" VARCHAR(128),
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_salesOrders_tb_idSalesOrder" PRIMARY KEY ("idSalesOrder")
);

ALTER TABLE public."salesOrders_tb"
ADD CONSTRAINT "FK_salesOrders_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrders_tb"
ADD CONSTRAINT "FK_salesOrders_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrders_tb"
ADD CONSTRAINT "FK_salesOrders_tb_TO_invoices_tb_idInvoice" FOREIGN KEY ("idInvoice")
REFERENCES public."invoices_tb" ("idInvoice") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrders_tb"
ADD CONSTRAINT "FK_salesOrders_tb_TO_salesOrderStatuses_tb_idSalesOrderStatus" FOREIGN KEY ("idSalesOrderStatus")
REFERENCES public."salesOrderStatuses_tb" ("idSalesOrderStatus") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrders_tb"
ADD CONSTRAINT "FK_salesOrders_tb_TO_salesOrderModes_tb_idSalesOrderMode" FOREIGN KEY ("idSalesOrderMode")
REFERENCES public."salesOrderModes_tb" ("idSalesOrderMode") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrders_tb"
ADD CONSTRAINT "FK_salesOrders_tb_TO_paymentMethods_tb_idPaymentMethod" FOREIGN KEY ("idPaymentMethod")
REFERENCES public."paymentMethods_tb" ("idPaymentMethod") ON UPDATE NO ACTION ON DELETE RESTRICT;


-- Creazione della tabella salesOrderItems_tb
CREATE TABLE IF NOT EXISTS public."salesOrderItems_tb" (
    "idSalesOrderItem" UUID NOT NULL,
    "idSalesOrder" UUID NOT NULL,
    "idProduct" UUID NOT NULL,
    "quantity" INTEGER NOT NULL, -- giusto per completezza - sempre 1
    "unitPrice" NUMERIC(18,2) NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_salesOrderItems_tb_idSalesOrderItem" PRIMARY KEY ("idSalesOrderItem")
);

ALTER TABLE public."salesOrderItems_tb"
ADD CONSTRAINT "FK_salesOrderItems_tb_TO_salesOrders_tb_idSalesOrder" FOREIGN KEY ("idSalesOrder")
REFERENCES public."salesOrders_tb" ("idSalesOrder") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrderItems_tb"
ADD CONSTRAINT "FK_salesOrderItems_tb_TO_products_tb_idProduct" FOREIGN KEY ("idProduct")
REFERENCES public."products_tb" ("idProduct") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrderItems_tb"
ADD CONSTRAINT "FK_salesOrderItems_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."salesOrderItems_tb"
ADD CONSTRAINT "FK_salesOrderItems_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;
