/// <reference types="vite/client" />
interface ImportMetaEnv {
  readonly VITE_API_URL: string;
  // Добавляй другие переменные, если есть
  // readonly VITE_IS_DEV: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}