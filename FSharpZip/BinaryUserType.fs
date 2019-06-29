module BinaryUserType


type UserBinaryThree = {
    right : UserBinaryThree
    left : UserBinaryThree
    weight : int
}

let threeCreator (numbers:list<int>) : UserBinaryThree =
    let getRightPart (nums:list<int>) index : list<int> = List.skip(nums.Length - index + 1)
    let getLeftPart (nums:list<int>) index :list<int> = List.take(nums.Length - index + 1)
    let getMiddle (nums:list<int>) = (nums.Length/2)
    let rec addToThree (nums:list<int>) = 
        if(nums)
    addToThree 
        