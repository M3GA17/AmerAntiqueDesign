-- Creazione della tabella invoices_tb
CREATE TABLE IF NOT EXISTS public."invoices_tb" (
    "idInvoice" UUID NOT NULL,
    "idSalesOrder" UUID NOT NULL,
    "filePath" VARCHAR(64) NOT NULL,
    "idInvoiceStatus" CHAR(5) NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_invoices_tb_idInvoice" PRIMARY KEY ("idInvoice")
);

ALTER TABLE public."invoices_tb"
ADD CONSTRAINT "FK_invoices_tb_TO_salesOrders_tb_idSalesOrder" FOREIGN KEY ("idSalesOrder")
REFERENCES public."salesOrders_tb" ("idSalesOrder") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."invoices_tb"
ADD CONSTRAINT "FK_invoices_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."invoices_tb"
ADD CONSTRAINT "FK_invoices_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."invoices_tb"
ADD CONSTRAINT "FK_invoices_tb_TO_invoiceStatuses_tb_idInvoiceStatus" FOREIGN KEY ("idInvoiceStatus")
REFERENCES public."invoiceStatuses_tb" ("idInvoiceStatus") ON UPDATE NO ACTION ON DELETE RESTRICT;
