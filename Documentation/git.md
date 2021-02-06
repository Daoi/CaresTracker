# Git Development Strategy

## Following this guide will help us all independently contribute code and keep our project functional at all times

### Basics
Here are some important commands you need to know:
- `git log`: shows you the commit history of your branch
- `git branch`: lists all of your local branches
- `git checkout <branch-name>`: lets you switch to a branch
    - ex. `git checkout develop`
- `git status`: shows the files which have uncommitted changes
- `git add <file-name>`: adds changes to the staging tree -- changes must be staged before they can be committed
- `git commit -m 'commit message'`: commits the staged changes along with a message detailing what they are
    - ex. `git commit -m 'add password encryption'`
    - try to make the number of changes per commit as small or as related as possible to simplify refactoring and bug hunting
- `git push`: this pushes your changes to the remote branch on GitHub -- it is important to make sure you are on the correct branch before pushing, so you don't overwrite the wrong work
    - if this is your first time pushing from this branch (ie. it does not exits on GitHub), you need to run `git push -u origin HEAD` instead, then you can use `git push` going forward
- `git pull`: this syncs the changes made to the remote branch into your local branch
    - you normally do not need to do this on your feature branches, since you'll be the only one making changes to it
    - this is mostly important for keeping the development branch updated -- this can be done by first switching to the development branch with `git checkout develop` then running `git pull`
- `git checkout -b <branch-name>`: creates a new local branch using the current branch you are on as a starting point 
    - ex. `git checkout -b login-system`

### The Strategy
Now that you know the basics, let's go over the branching strategy we'll use. This is commonly known as [Gitflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow). It involves three kinds of branches: 
- **main** (or production): the code that is deployed for use
- **develop**: the branch the developers collectively work on during sprints
- **feature**: the branches in which developers write new features after branching off of the development branch

There is only one main branch and one develop branch, while there may be many feature branches, which branch off of develop. Each new feature should be coded in its own feature branch. 
```git
# make sure to get develop's latest changes
git checkout develop
git pull

# make a new branch with a name related to your feature
git checkout -b file-upload
```
Now you can push this new local branch to GitHub whenever you are ready.
```git
# run this for the first push
# make sure you are in the feature branch
git push -u origin HEAD
```
Remember: after the first push you can simply use: `git push`

When a feature has been finished and tested locally, it can be merged into develop by using GitHub's Pull Request feature. 

First, make sure to push your latest changes. Then, go to your feature branch on GitHub, and click "Pull Request" under the green "Code" button. Make sure to always set the **base** branch on the left to **develop** and the **compare** branch to your feature branch. (Note: features are never merged directly into **main**)

Give the pull request a meaningful title, and leave a comment about what your new code does. On the left, side you can assign reviewers to look over your code.

When you're ready, hit the "Create Pull Request" button. Now that the pull request is made, it must be reviewed by a team member to give feedback and test for bugs. Once testing is done and everything's working fine in the feature branch, the feature branch can be safely merged into **develop**. (Note: at this point everyone would need to pull from develop to get the new changes)
