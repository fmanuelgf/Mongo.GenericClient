Feature: Update a Collection - Service

  Scenario Outline: Can UpdateOneAsync by modifying the entity
    Given a WriteService of PersonEntity
    And a collection of 1 person, with name <name> and age <age> exist
    And the PersonEntity.<fieldToUpdate> is modified to <newValue>
    When calling the UpdateAsync method of the WriteService
    Then person.<fieldToUpdate> in the collection equals <newValue>

    Examples:
      | name  | age | fieldToUpdate | newValue |
      | John  | 20  | Name          | Frank    |
      | John  | 20  | Age           | 30       |
      | Frank | 30  | Name          | Tom      |
      | Frank | 30  | Age           | 40       |

  Scenario Outline: Can UpdateOneAsync by field-value pairs
    Given a WriteService of PersonEntity
    And a collection of 1 person, with name <name> and age <age> exist
    And using the field-value pair <fieldToUpdate> with value <newValue>
    And using the person's ID as <personIdType>
    When calling the UpdateAsync method of the WriteService using the person's ID and the field-value pairs
    Then person.<fieldToUpdate> in the collection equals <newValue>
    Then person.<unchangedField> in the collection equals <unchangedValue>

    Examples:
      | name  | age | personIdType | fieldToUpdate | newValue | unchangedField | unchangedValue |
      | John  | 20  | ObjectId     | Name          | Frank    | Age            | 20             |
      | John  | 20  | string       | Age           | 30       | Name           | John           |
      | Frank | 30  | ObjectId     | Name          | Tom      | Age            | 30             |
      | Frank | 30  | string       | Age           | 40       | Name           | Frank          |