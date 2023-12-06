export class RecipeFavorite {
  constructor(data) {
    this.favoriteId = data.favoriteId
    this.accountId = data.accountId
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}

// const recipeFavorite = {
//   "favoriteId": 3,
//   "accountId": "652ef9f6df6d033d3abe329a",
//   "id": 26,
//   "createdAt": "2023-12-01T21:15:09",
//   "updatedAt": "2023-12-01T21:15:09",
//   "title": "The Senior Jake Burrito",
//   "instructions": "Take your burrito and cook it, then add hotdog.  Boom, Burrito.",
//   "img": "https://images.unsplash.com/photo-1584031036380-3fb6f2d51880?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=900&q=80",
//   "category": "Mexican",
//   "creatorId": "652ef9f6df6d033d3abe329a",
//   "creator": null
// }