export const formatDate = (iso: string) => {
  const [datePart] = iso.split(" ");
  const [day, month, year] = datePart.split(".");

  // Собираем в формате, который поймет Date (YYYY-MM-DD)
  const isoDate = `${year}-${month}-${day}`;
  return new Date(isoDate).toLocaleDateString("ru-RU", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });
};

export const formatDate2 = (iso: string) => {
  return new Date(iso).toLocaleDateString("ru-RU", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });
};

