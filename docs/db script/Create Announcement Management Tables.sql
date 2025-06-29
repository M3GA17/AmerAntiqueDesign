-- Creazione della tabella announcements_tb
CREATE TABLE IF NOT EXISTS public."announcements_tb" (
    "idAnnouncement" UUID NOT NULL,
    "title" VARCHAR(255) NOT NULL,
    "description" TEXT NOT NULL,
    "address" VARCHAR(255),
    "eventStartDateTime" TIMESTAMP WITH TIME ZONE NOT NULL,
    "eventEndDateTime" TIMESTAMP WITH TIME ZONE,
    "idAnnouncementType" CHAR(5) NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_announcements_tb_idAnnouncement" PRIMARY KEY ("idAnnouncement")
);
ALTER TABLE public."announcements_tb"
ADD CONSTRAINT "FK_announcements_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."announcements_tb"
ADD CONSTRAINT "FK_announcements_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."announcements_tb"
ADD CONSTRAINT "FK_announcements_tb_TO_announcementTypes_tb_idAnnouncementType" FOREIGN KEY ("idAnnouncementType")
REFERENCES public."announcementTypes_tb" ("idAnnouncementType") ON UPDATE NO ACTION ON DELETE RESTRICT;


-- Creazione della tabella announcementPhotos_tb
CREATE TABLE IF NOT EXISTS public."announcementPhotos_tb" (
    "idAnnouncementPhotos" UUID NOT NULL,
    "idAnnouncement" UUID NOT NULL,
    "name" VARCHAR(512) NOT NULL,
    "url" VARCHAR(2048) NOT NULL,
    "displayOrder" INTEGER NOT NULL,
    "idUserCreate" UUID NOT NULL,
    "dateCreate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "idUserUpdate" UUID NOT NULL,
    "dateUpdate" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "PK_announcementPhotos_tb_idAnnouncementPhotos" PRIMARY KEY ("idAnnouncementPhotos")
);

ALTER TABLE public."announcementPhotos_tb"
ADD CONSTRAINT "FK_announcementPhotos_tb_TO_announcements_tb_idAnnouncement" FOREIGN KEY ("idAnnouncement")
REFERENCES public."announcements_tb" ("idAnnouncement") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."announcementPhotos_tb"
ADD CONSTRAINT "FK_announcementPhotos_tb_TO_users_tb_idUserCreate" FOREIGN KEY ("idUserCreate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE public."announcementPhotos_tb"
ADD CONSTRAINT "FK_announcementPhotos_tb_TO_users_tb_idUserUpdate" FOREIGN KEY ("idUserUpdate")
REFERENCES public."users_tb" ("idUser") ON UPDATE NO ACTION ON DELETE RESTRICT;