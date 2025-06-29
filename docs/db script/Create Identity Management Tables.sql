CREATE TABLE IF NOT EXISTS public."users_tb" (
    "idUser" UUID NOT NULL,
    "username" VARCHAR,
    CONSTRAINT "PK_users_tb_idUser" PRIMARY KEY ("idUser")
);