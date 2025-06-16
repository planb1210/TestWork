document.addEventListener('DOMContentLoaded', function () {
    postData()
        .then(data => {
            console.log(data);
            renderCourses(data);
        })
        .catch(error => console.error('Error:', error));
});

function getFilterValues() {
    const subject = document.getElementById('subject').value.trim();
    const grade = document.getElementById('grade').value.trim();
    const genre = document.getElementById('genre').value;

    const filters = {
        subject: subject || null,
        grade: grade || null,
        genre: genre || null
    };

    postData(filters)
        .then(data => {
            console.log(data);
            renderCourses(data);
        })
        .catch(error => console.error('Error:', error));
}

async function postData(data = {}) {

    const url = 'http://localhost:5203/TestWorkPhysicon/Courses';

    try {
        console.log(data);
        console.log(JSON.stringify(data));

        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}

function renderCourses(courses) {
    const container = document.getElementById('coursesContainer');
    container.innerHTML = '';

    const ul = document.createElement('ul');
    ul.className = 'tree';

    courses.forEach(course => {
        const li = document.createElement('li');

        const courseDiv = document.createElement('div');
        courseDiv.className = 'course';
        courseDiv.innerHTML = `${course.title}`;
        li.appendChild(courseDiv);

        if (course.modules && course.modules.length > 0) {
            const modulesUl = document.createElement('ul');
            renderModules(course.modules, modulesUl);
            li.appendChild(modulesUl);
        }

        ul.appendChild(li);
    });

    container.appendChild(ul);
}

function renderModules(modules, parentElement, level = 0) {
    modules.sort((a, b) => a.order - b.order);

    modules.forEach(module => {
        const li = document.createElement('li');
        li.className = 'module';

        const moduleDiv = document.createElement('div');
        moduleDiv.innerHTML = `${module.num}${module.title}`;
        const hasChildren = module.modules && module.modules.length > 0;

        if (hasChildren) {
            const childUl = document.createElement('ul');
            renderModules(module.modules, childUl, level + 1);
            li.appendChild(moduleDiv);
            li.appendChild(childUl);
        } else {
            li.appendChild(moduleDiv);
        }

        parentElement.appendChild(li);
    });
}