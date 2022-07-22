import os
import re
import shutil

directory = os.path.dirname(os.path.abspath(__file__))
pattern = r' ([0-9]{2})\.([0-9]{2})\.([0-9]{4}) \w+.png'

onlyfiles = [filename for filename in os.listdir(directory) if os.path.isfile(os.path.join(directory, filename))]

if __name__ == '__main__':
    for filename in onlyfiles:
        gameDirectory = re.sub(pattern, '', filename)

        if gameDirectory == 'main.py':
            break

        if gameDirectory == 'desktop.ini':
            break

        try:
            os.mkdir(gameDirectory)
        except FileExistsError:
            print('Directory ', gameDirectory, " already exists")

        os.rename(f'{directory}\\{filename}', f'{directory}\\{gameDirectory}\\{filename}')
