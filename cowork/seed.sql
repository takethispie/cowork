INSERT INTO public."SubscriptionType" ("Id", "Name", "FixedContractDurationMonth", "PriceFirstHour", "PriceNextHalfHour", "PriceDay", "PriceDayStudent", "FixedContractMonthlyFee", "ContractFreeMonthlyFee", "Description") VALUES (1, 'Résident', 8, 0, 0, 0, 0, 252, 300, 'Rejoignez la communauté
CO''WORK et devenez membre
résident !');
INSERT INTO public."SubscriptionType" ("Id", "Name", "FixedContractDurationMonth", "PriceFirstHour", "PriceNextHalfHour", "PriceDay", "PriceDayStudent", "FixedContractMonthlyFee", "ContractFreeMonthlyFee", "Description") VALUES (0, 'Simple', 12, 4, 2, 20, 20, 20, 24, 'Rejoignez la communauté
CO''WORK et bénéficiez de
tarifs préférentiels !');

INSERT INTO public."Place" ("Id", "Name", "HighBandwidthWifi", "MembersOnlyArea", "UnlimitedBeverages", "CosyRoomAmount", "LaptopAmount", "PrinterAmount") VALUES (0, 'Bastille', true, true, true, 1, 0, 1);

INSERT INTO public."Login" ("Id", "PasswordHash", "PasswordSalt", "UserId", "Email") VALUES (3, E'\\xF37408CD9F182452DEB14FB2E17A9C9F1429B0B25462043B74D2AD49FCB8B7F05B5E89DB030A77BC8106B401480CE4A3DB462A6062AE32B1548DA35A115DBD55', E'\\x590FE0812F36DBB87AF993A573178223A067FCA3036214C805A9A7A9B70D6408F078440733EF83CBF8D439C7AB4E50611B56DFDF5CD7DEA08E001B78740E1B52B5609C1BCC2EEB954CDE3127A8954CB201694DAAA309F0F61407E281658913FE60AC420D96AFFF486C8BDE0B19DB877691674B3E348A88C4E2676C28ECCD445D', 136, 'takethispie');

INSERT INTO public."Users" ("Id", "FirstName", "LastName", "IsStudent", "Type") VALUES (136, 'alexandre', 'felix', false, 0);

INSERT INTO public."SubscriptionType" ("Id", "Name", "FixedContractDurationMonth", "PriceFirstHour", "PriceNextHalfHour", "PriceDay", "PriceDayStudent", "FixedContractMonthlyFee", "ContractFreeMonthlyFee", "Description") VALUES (1, 'Résident', 8, 0, 0, 0, 0, 252, 300, 'Rejoignez la communauté
CO''WORK et devenez membre
résident !');
INSERT INTO public."SubscriptionType" ("Id", "Name", "FixedContractDurationMonth", "PriceFirstHour", "PriceNextHalfHour", "PriceDay", "PriceDayStudent", "FixedContractMonthlyFee", "ContractFreeMonthlyFee", "Description") VALUES (0, 'Simple', 12, 4, 2, 20, 20, 20, 24, 'Rejoignez la communauté
CO''WORK et bénéficiez de
tarifs préférentiels !');

INSERT INTO public."Subscription" ("Id", "TypeId", "LatestRenewal", "UserId", "PlaceId", "FixedContract") VALUES (10, 0, '2019-08-31 13:43:38.044000', 136, 0, false);

