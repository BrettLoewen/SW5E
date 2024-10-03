import fs from "fs";
import process from "process";

const languages = ["en", "sp", "ru", "gu"];

const endpoints = [
    "archetype",
    "ArmorProperty",
    "background",
    "characterAdvancement",
    "class",
    "ClassImprovement",
    "conditions",
    //"Credit",
    "DataVersion",
    "enhancedItem",
    "equipment",
    "ExpandedContent",
    "Feat",
    "Feature",
    "FightingMastery",
    "FightingStyle",
    "LightsaberForm",
    "Maneuvers",
    "Monster",
    "MulticlassImprovement",
    "playerHandbookRule",
    "power",
    "ReferenceTable",
    "skills",
    "species",
    "SplashclassImprovement",
    "StarshipBaseSize",
    "StarshipDeployment",
    "StarshipEquipment",
    "StarshipModification",
    "StarshipRule",
    "StarshipVenture",
    "VariantRule",
    "WeaponFocus",
    "WeaponProperty",
    "WeaponSupremacy",
    "wretchedHivesRule",
];

const language = process.argv[2];

getData(language);

async function getData(language = "en") {
    try {
        if (!languages.includes(language)) {
            throw new Error(
                `Invalid language: ${language}. The available options are ${languages}`
            );
        }

        console.log("Fetching data");

        await endpoints.map(async (endpoint) => {
            const url = `https://sw5eapi.azurewebsites.net/api/${endpoint}?language=${language}`;

            const response = await fetch(url);

            if (!response.ok) {
                throw new Error(
                    `Response status: ${response.status} on ${endpoint}`
                );
            }

            const json = await response.json();
            // console.log(json);

            const data = JSON.stringify(json, undefined, 4);

            fs.writeFile(`apiData/${endpoint}.json`, data, (error) => {
                if (error) {
                    throw error;
                }
            });
        });

        console.log("Data written correctly");
    } catch (error) {
        console.error(error.message);
    }
}
