module AdventOfCode2023.Tests

open NUnit.Framework
open AdventOfCode2023.Cube

[<SetUp>]
let Setup () = ()

[<Test>]
let Test1 () = Assert.Pass()


[<Test>]
let parseGameId_Should_return_123 () =
    //Arrange
    let input = "GAME 123"
    //Act
    let res = parseGameId input
    //Assert
    Assert.AreEqual(Some(123), res)


[<Test>]
let parseGameId_Should_return_None () =
    //Arrange
    let input = "GAME"
    //Act
    let res = parseGameId input
    //Assert
    Assert.AreEqual(None, res)

[<Test>]
let parseRedCube_Should_return_15 () =
    //Arrange
    let input = "16 green, 18 blue, 15 red"
    //Act
    let res = parseRedCube input
    //Assert
    Assert.That(res, Is.EqualTo(Some(15)))

[<Test>]
let parseGreenCube_Should_return_16 () =
    //Arrange
    let input = "16 green, 18 blue, 15 red"
    //Act
    let res = parseGreenCube input
    //Assert
    Assert.That(res, Is.EqualTo(Some(16)))


[<Test>]
let parseBlueCube_Should_return_18 () =
    //Arrange
    let input = "16 green, 18 blue, 15 red"
    //Act
    let res = parseBlueCube input
    //Assert
    Assert.That(res, Is.EqualTo(Some(18)))

[<Test>]
let parseRecord_Should_Return_CorrectRecords () =
    //Arrange
    let input = "16 green, 18 blue, 15 red"

    let expected: Record =
        { BlueCube = Some(18)
          GreenCube = Some(16)
          RedCube = Some(15) }

    //Act
    let res = parseRecord input

    //Assert
    Assert.That(res, Is.EqualTo(expected))

[<Test>]
let parseRecord_Should_Return_CorrectRecordsWithNone () =
    //Arrange
    let input = "16 green, 15 red"

    let expected: Record =
        { BlueCube = None
          GreenCube = Some(16)
          RedCube = Some(15) }

    //Act
    let res = parseRecord input

    //Assert
    Assert.That(res, Is.EqualTo(expected))


[<Test>]
let parseGame_Should_ReturnCorrectValue () =
    //Arrange
    let input = "Game 5: 2 red, 1 blue, 4 green; 6 blue, 2 green; 2 red, 5 green"

    let expectedOutput: Game =
        { Id = 5
          Records =
            [ { RedCube = Some(2)
                BlueCube = Some(1)
                GreenCube = Some(4) }
              { RedCube = None
                BlueCube = Some(6)
                GreenCube = Some(2) }
              { RedCube = Some(2)
                BlueCube = None
                GreenCube = Some(5) } ] }
    //Act
    let res = parseGame input
    //Assert
    Assert.That(res, Is.EqualTo(expectedOutput))

[<Test>]
let getMinimumRequiredConfiguration_return_consistent_configuration () =
    //Arrange
    let input: Game =
        { Id = 1
          Records =
            [ { BlueCube = Some(3)
                RedCube = Some(4)
                GreenCube = None }
              { RedCube = Some(1)
                GreenCube = Some(2)
                BlueCube = Some(6) }
              { GreenCube = Some(2)
                RedCube = None
                BlueCube = None } ] }

    let expectedOutput: Record =
        { RedCube = Some(4)
          GreenCube = Some(2)
          BlueCube = Some(6) }

    //Act
    let res = getMinimumRequiredConfiguration input

    //Assert
    Assert.That(res, Is.EqualTo(expectedOutput))
