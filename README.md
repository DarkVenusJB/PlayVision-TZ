<div id="header" align="center">
  <h1>  Тестовое задание на позицию Unity Developer </h1>
</div>

# Задача:

Необходимо создать сцену, где кубики падают с реальной физикой, но значение, которое на них выпадает, должно быть известно заранее. 

# Решение:

![Unity_6tVXShLx0i](https://github.com/DarkVenusJB/PlayVision-TZ/assets/91538380/de4db530-e42b-417a-8922-cfd42d0dad76)

<div id="header" align="center">
  <h1>  Документация  </h1>
</div> 

<div id="header" align="center">
  <h2>  Класс DiceDroper  </h2>
</div> 

### Данный класс является основным и реализует симуляцию броска кубиков, с заранее известными значениями

<div id="header" align="center">
  <h3>  Методы инициализации кубиков  </h3>
</div> 

- InitializeDicesState(): Задает начальные положение, вращение и физические характеристики кубиков.
- InitializeRotation(), InitializeForce(), InitializeTorque(): Вычисляют случайные значения для начального вращения, силы и крутящего момента кубиков.

<div id="header" align="center">
  <h3>  Методы управления физикой  </h3>
</div> 

- EnablePhysics(), DisablePhysics(): Включают и выключают физические свойства кубиков.

<div id="header" align="center">
  <h3>  Методы записи и воспроизведения анимаций   </h3>
</div> 

- RecordAnimation(): Записывает положение и вращение кубиков в каждом кадре симуляции.
- PlayAnimation(): Воспроизводит записанную анимацию.
- ClearAnimationRecordData(): Очищает данные записанной анимации.

<div id="header" align="center">
  <h3>  Главный метод DropDice()   </h3>
</div> 

Публичный метод, который запускает симуляцию броска, поворачивает кубики нужной стороной и воспроизводит анимацию. Медод вызывается из класса GameUI

<div id="header" align="center">
  <h3>  Метод вращения кубиков RotateDices()  </h3>
</div> 

Метод, который перебирает все кубики и вызывает метод RotateDiceMesh из класса DiceRotator

<div id="header" align="center">
  <h2>  Вспомогательный класс AnimationRecordData  </h2>
</div> 

### Класс, хранит данные о положении и вращении кубика в каждом кадре анимации. Имеет внутри себя конструктор для инициализации значений и свойства доступные только для чтения

<div id="header" align="center">
  <h2>  Класс DiceRotator  </h2>
</div> 

### Класс отвечает за вращения меша внутри кубика

<div id="header" align="center">
  <h3>  Методы вращения мешей   </h3>
</div> 

- ResetMeshRotation(): сбрасывает вращение до начального значения
- RotateDiceMesh(): осуществляет поворот к нужому положению которое передаётся из класса GameUI

<div id="header" align="center">
  <h2>  Класс DiceRotationData  </h2>
</div> 

### Класс представляет из себя Scriptable Object, который хранит в себе значения rotation для каждой грани, в наивысшем положении

<div id="header" align="center">
  <h2>  Класс GameUI  </h2>
</div> 

### Класс отвечает за запуск броска кубиков и выбор значений

<div id="header" align="center">
  <h3>  Методы получения верных значений   </h3>
</div> 

Методы GetFirstValue() и GetSecondValue() получают значния из InputFied, преобразуют их в int, после чего проходит проверка на соответствие значения и номера на кубике. Если таких значений нет, то ставится значение по умолчанию, равное 1

<div id="header" align="center">
  <h3>  Метод запуска броска StartDrop()   </h3>
</div>

Метод запускает симуляцию броска кубика с переданными ему значениями, верных граней. Запуск происходит путём вызова метода DropDice() из класса DiceDroper
