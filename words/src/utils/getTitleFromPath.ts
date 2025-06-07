const getTitleByPath = (path: string): string => {
  switch (path) {
    case "/":
      return "Выберите режим";
    case "/dictionary":
      return "Словарь";
    case "/result":
      return "Результат проверки знаний";
    case "/check":
      return "Проверка знаний";
    case "/new-word":
      return "Добавление слова";
    case "/edit-word":
      return "Редактирование слова";
    default:
      return "Страница";
  }
};

export default getTitleByPath;
