import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";

import kk from "./resources_kk.json";
import ru from "./resources_ru.json";

i18n
  .use(LanguageDetector) // автоматическое определение языка
  .use(initReactI18next)
  .init({
    resources: {
      kk: { translation: kk },
      ru: { translation: ru },
    },
    fallbackLng: "ru",
    interpolation: {
      escapeValue: false, // для React не нужно
    },
  });

export default i18n;
