# Git Development Strategy

## Following this guide will help us all independently contribute code and keep our project functional at all times

### Basics
Here are some important commands you need to know:
- `git log`: shows you the commit history of your branch
- `git branch`: lists all of your local branches
- `git checkout <branch-name>`: lets you switch to a branch
    - ex. `git checkout develop`
- `git stash`: you cannot switch between branches if you have uncommitted changes in your local branch, so if you don't want to commit them you can "save them" by running this command
    - if you want to get your changes back, run `git stash pop` after returning to the branch
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
- `git merge <branch-name>`: merges the specified branch into the branch you are currently working in

### The Strategy
Now that you know the basics, let's go over the branching strategy we'll use. This is commonly known as [Gitflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow). It involves three kinds of branches: 
- **main** (or production): the code that is deployed for use
- **develop**: the branch the developers collectively work on during sprints
- **feature**: the branches in which developers write new features after branching off of the development branch

There is only one main branch and one develop branch, while there may be many feature branches, which branch off of develop. Each new feature should be coded in its own feature branch. 
```git
# skip this command if you already have develop locally
git fetch

# continue
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

#### What if someone else merges new changes into develop while I'm working on a feature branch?
You won't see these changes in your feature branch because you branched off of develop *before* those changes were there. In order to get them into your feature branch, you'll need to merge develop into it:
```git
# get the latest changes to develop
git checkout develop
git pull

# merge develop into your feature branch
git checkout my-feature-branch
git merge develop
```
I recommend only doing this once you're ready to create a pull request, unless you're certain you need to use the new code for your work. This should work fine as long as you didn't modify any lines of code that were already modified in one of the new changes you just merged in. In the next section, we'll cover how to handle these Merge Conflicts. 

### Handling [Merge Conflicts](https://www.atlassian.com/git/tutorials/using-branches/merge-conflicts)
This is the main reason Git exists and what makes it so useful. Here's a brief summary from the linked article:
> Conflicts generally arise when two people have changed the same lines in a file.

> In these cases, Git cannot automatically determine what is correct. Conflicts only affect the developer conducting the merge, the rest of the team is unaware of the conflict. Git will mark the file as being conflicted and halt the merging process. It is then the developers' responsibility to resolve the conflict.

Let's use a simple example. There's a file called `filename.text` that a teammate and I are editing in our separate feature branches. He makes a change to the file, adding:
```text
I am Coder A and I wrote this.
```
He commits this to his feature branch and merges it into develop.

At the same time, I committed this change to my local feature branch on the <ins>*same*</ins> line:
```
I, Coder B, wrote this to the same line.
```

At this point I see that there are new changes to develop, and I want to merge them into my feature branch. <ins>After checking out develop and pulling the latest changes</ins>, I head <ins>back to my feature branch</ins> to perform the merge. The error message from the merge conflict will look like:
```git
git merge develop
Auto-merging filename.txt
CONFLICT (content): Merge conflict in filename.txt
Automatic merge failed; fix conflicts and then commit the result.
```
Git will then add some text to the file to show you what the merge conflict is. Open it up in any text editor and you should see the added markers with both changes (HEAD is your change):
```text
<<<<<< HEAD
I, Coder B, wrote this to the same line.
=======
I am Coder A and I wrote this.
>>>>>> develop
```
(I recommend Visual Studio Code because it has nicer UI tools to deal with changes, but normal Visual Studio works too)

You need to remove the markers and save the file to be able to continue. 

You can choose to keep one change:
```text
I am Coder A and I wrote this.
```
or
```text
I, Coder B, wrote to this to the same line.
```

**Or**, you can modify both to get a combination/something completely new:
```text
We are Coders A & B. We worked on this together!
```
In either case, remember to completely remove the text that Git added to mark where your HEAD and incoming branch changes were. You will have to repeat this process for all merge conflicts across multiple files if necessary.

Once you've <ins>fixed all the conflicts *and* saved</ins> all the files using your text editor, return to your terminal and commit the change:
```text
git commit -a
```
This may open up a default text editor that you'll need to save and exit from. Here are instructions for [nano](https://wiki.gentoo.org/wiki/Nano/Basics_Guide#Saving_and_exiting) and [vi](https://www.howtogeek.com/411210/how-to-exit-the-vi-or-vim-editor/).

After saving the commit message, the merge process will be complete, and your branch will be ready for a pull request or to keep working in.
