CREATE TABLE IF NOT EXISTS public."productStatuses_tb" (
    "idProductStatus" CHAR(5) NOT NULL,
    "statusName" VARCHAR(128),
    CONSTRAINT "PK_productStatuses_tb_idProductStatus" PRIMARY KEY ("idProductStatus")
);

CREATE TABLE IF NOT EXISTS public."salesOrderStatuses_tb" (
    "idSalesOrderStatus" CHAR(5) NOT NULL,
    "description" VARCHAR(128),
    CONSTRAINT "PK_salesOrderStatuses_tb_idSalesOrderStatus" PRIMARY KEY ("idSalesOrderStatus")
);

CREATE TABLE IF NOT EXISTS public."salesOrderModes_tb" (
    "idSalesOrderMode" CHAR(5) NOT NULL,
    "description" VARCHAR(128),
    CONSTRAINT "PK_salesOrderModes_tb_idSalesOrderMode" PRIMARY KEY ("idSalesOrderMode")
);

CREATE TABLE IF NOT EXISTS public."paymentMethods_tb" (
    "idPaymentMethod" CHAR(5) NOT NULL,
    "description" VARCHAR(128),
    CONSTRAINT "PK_paymentMethods_tb_idPaymentMethod" PRIMARY KEY ("idPaymentMethod")
);

CREATE TABLE IF NOT EXISTS public."announcementTypes_tb" (
    "idAnnouncementType" CHAR(5) NOT NULL,
    "description" VARCHAR(128),
    CONSTRAINT "PK_announcementTypes_tb_idAnnouncementType" PRIMARY KEY ("idAnnouncementType")
);

CREATE TABLE IF NOT EXISTS public."invoiceStatuses_tb" (
    "idInvoiceStatus" CHAR(5) NOT NULL,
    "description" VARCHAR(128),
    CONSTRAINT "PK_invoiceStatuses_tb_idInvoiceStatus" PRIMARY KEY ("idInvoiceStatus")
);