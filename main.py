import os
import re

directory = os.path.dirname(os.path.abspath(__file__))
pattern = r' ([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.(png|mp4)'

onlyfiles = [filename for filename in os.listdir(directory) if filename.lower().endswith(('.png', '.mp4'))]


def create_folders(list_of_names: list) -> None:
    """
    Функция получает массив с названиями тайтлов
    и создает для каждого тайтла папку с этим названием

    :param list_of_names:
    """
    for title in list_of_names:
        try:
            os.mkdir(title)
        except OSError:
            print('Что-то пошло не так')


if __name__ == '__main__':
    print('Запуск...')

    # получаем массив с названиями игр по регулярному выражению
    game_directories = list(set([re.sub(pattern, '', filename) for filename in onlyfiles]))

    # создаем папку для каждой игры
    print('Создаем папки...')
    create_folders(game_directories)

    # переносим все файлы относящие к текущей игре
    print('Переносим файлы...')
    for directory_name in game_directories:
        for filename in onlyfiles:
            if directory_name in filename:
                os.rename(f'{directory}\\{filename}', f'{directory}\\{directory_name}\\{filename}')
    print('Готово!')
